using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Paged;
using OngProject.Core.Models.PagedResourceParameters;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class TestimonialsService : ITestimonialsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContext;

        public TestimonialsService(IUnitOfWork unitOfWork, IImageService imageService, IEntityMapper entityMapper, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = entityMapper;
            _imageService = imageService;
            _httpContext = httpContext;
        }

        public async Task<Result> GetAll(PaginationParams paginationParams)
        {
            try
            {
                var testimonials = await _unitOfWork.TestimonialsRepository.FindAllAsync(null, null, null, paginationParams.PageNumber, paginationParams.PageSize);
                var totalCount = await _unitOfWork.TestimonialsRepository.Count();

                if (totalCount == 0)
                {
                    return Result.FailureResult("No existen testimonios");
                }

                if (testimonials.Count == 0)
                {
                    return Result.FailureResult("Paginación inválida, no hay resultados");
                }             

                var paged = PagedList<Testimonials>.Create(testimonials.ToList(), totalCount, paginationParams.PageNumber, paginationParams.PageSize);

                var url = $"{this._httpContext.HttpContext.Request.Scheme}://{this._httpContext.HttpContext.Request.Host}{this._httpContext.HttpContext.Request.Path}";
                var pagedResponse = new PagedResponse<Testimonials>(paged, url);

                return Result<PagedResponse<Testimonials>>.SuccessResult(pagedResponse);
            }
            catch (Exception ex)
            {
                return Result.ErrorResult(new List<string> { ex.Message });
            }
        }

        public Testimonials GetById()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Insert(TestimonialDTO testimonialDTO)
        {
            try
            {
                var testimonial = _mapper.TestimonialDTOToTestimonial(testimonialDTO);

                var resultName = await _unitOfWork.TestimonialsRepository.FindByConditionAsync(x => x.Name == testimonialDTO.Name);

                if (resultName.Count == 0)
                {
                    var aws = new S3AwsHelper();
                    var result = await _imageService.UploadFile($"{Guid.NewGuid()}{testimonialDTO.File.FileName}", testimonialDTO.File);

                    testimonial.Image = result;
                    testimonial.SoftDelete = false;
                    testimonial.LastModified = DateTime.Now;

                    await _unitOfWork.TestimonialsRepository.Create(testimonial);
                    await _unitOfWork.SaveChangesAsync();

                    var testimonialDisplay = _mapper.TestimonialDTOToTestimonialDisplay(testimonialDTO);
                    testimonialDisplay.Image = result;

                    return Result<TestimonialDTODisplay>.SuccessResult(testimonialDisplay);
                }
                else
                {
                    throw new Exception("El nombre del testimonio ya existe en el sistema, intente uno diferente al ingresado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Testimonio no registrado: " + ex.Message);
            }
        }

        public void Update(Testimonials testimonials)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var testimonial = await _unitOfWork.TestimonialsRepository.GetByIdAsync(id);
                if (testimonial is not null)
                {
                    if (testimonial.SoftDelete)
                        return Result.FailureResult("El testimonio ya se encuentra eliminado del sistema");

                    testimonial.SoftDelete = true;
                    testimonial.LastModified = DateTime.Now;
                    await this._unitOfWork.SaveChangesAsync();

                    var testimonialDTO = _mapper.TestimonialToTestimonialDTO(testimonial);

                    return Result<string>.SuccessResult("Testimonio eliminado.");
                }

                return Result.FailureResult("No existe un testimonio con ese Id");
            }
            catch (Exception ex)
            {
                return Result.ErrorResult(new List<string> { ex.Message });
            }
        }
    }
}
