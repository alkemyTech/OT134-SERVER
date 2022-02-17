using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class OrganizationService : IOrganizationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly IImageService _imageService;
        private readonly ISlideSerivice _slideSerivice;

        public OrganizationService(IUnitOfWork unitOfWork, IEntityMapper mapper, ISlideSerivice slideSerivice, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _slideSerivice = slideSerivice;
            _imageService= imageService;
        }

        public void Delete(Organization organization)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrganizationDTOForDisplay>> GetAll()
        {
            var response = await _unitOfWork.OrganizationRepository.FindByConditionAsync(x => !x.SoftDelete);

            List<OrganizationDTOForDisplay> organizationDtoForDisplay = new();

            if (response.Count > 0)
            {
                foreach (var entity in response)
                {
                    var orgDto = _mapper.OrganizationToOrganizationDTOForDisplay(entity);

                    orgDto.Slides = await _slideSerivice.GetAllByOrganization(entity.Id);

                    organizationDtoForDisplay.Add(orgDto);
                }

                return organizationDtoForDisplay;
            }
            else
                return null;
        }

        public Organization GetById()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Insert(OrganizationDTOForUpload organizationDTOForUpload)
        {
            try
            {
                var organization = _mapper.OrganizationDtoForUploadtoOrganization(organizationDTOForUpload);

                var ValidationName = await _unitOfWork.OrganizationRepository.FindByConditionAsync(x => x.Name == organizationDTOForUpload.Name);
                if (ValidationName.Count > 0)
                    throw new Exception("Una organizacion con ese nombre ya existe en el sistema, intente uno diferente al ingresado.");
                else
                {
                    var imageUploadUrl = await _imageService.UploadFile(organizationDTOForUpload.Image.FileName, organizationDTOForUpload.Image);
                    organization.Image = imageUploadUrl;
                    organization.LastModified = DateTime.Today;

                    await _unitOfWork.OrganizationRepository.Create(organization);
                    await _unitOfWork.SaveChangesAsync();

                    var organizationDtoForDisplay = _mapper.OrganizationToOrganizationDTOForDisplay(organization);

                    return Result<OrganizationDTOForDisplay>.SuccessResult(organizationDtoForDisplay);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Result> Update(int id, OrganizationDTOForUpload organizationDTOForUpload)
        {
            try
            {
                var organization = await _unitOfWork.OrganizationRepository.GetByIdAsync(id);

                if (organization is not null)
                {
                    if (organizationDTOForUpload.Name != null)
                        organization.Name = organizationDTOForUpload.Name;
                    if (organizationDTOForUpload.Image != null)
                    {
                        await _imageService.AwsDeleteFile(organization.Image[(organization.Image.LastIndexOf("/") + 1)..]);
                        var imageUploadUrl = await _imageService.UploadFile(organizationDTOForUpload.Image.FileName, organizationDTOForUpload.Image);
                        organization.Image = imageUploadUrl;
                    }
                    if (organizationDTOForUpload.Address != null)
                        organization.Address = organizationDTOForUpload.Address;
                    if (organizationDTOForUpload.Phone != null)
                        organization.Phone = organizationDTOForUpload.Phone;
                    if (organizationDTOForUpload.Email != null)
                        organization.Email = organizationDTOForUpload.Email;
                    if (organizationDTOForUpload.WelcomeText != null)
                        organization.WelcomeText = organizationDTOForUpload.WelcomeText;
                    if (organizationDTOForUpload.AboutUsText != null)
                        organization.AboutUsText= organizationDTOForUpload.AboutUsText;
                    if (organizationDTOForUpload.FacebookUrl != null)
                        organization.FacebookUrl = organizationDTOForUpload.FacebookUrl;
                    if (organizationDTOForUpload.InstagramUrl != null)
                        organization.InstagramUrl = organizationDTOForUpload.InstagramUrl;
                    if (organizationDTOForUpload.LinkedinUrl != null)
                        organization.LinkedinUrl = organizationDTOForUpload.LinkedinUrl;
                    this._unitOfWork.OrganizationRepository.Update(organization);
                    await this._unitOfWork.SaveChangesAsync();
                    var organizationDTOForDisplayy = _mapper.OrganizationToOrganizationDTOForDisplay(organization);
                    return Result<OrganizationDTOForDisplay>.SuccessResult(organizationDTOForDisplayy);
                }
                return Result.FailureResult("id de usuario inexistente.", 404);
            }
            catch (Exception ex)
            {
                return Result.FailureResult("Error al actualizar la organizacion: " + ex.Message, 500);
            }
        }
    }
}