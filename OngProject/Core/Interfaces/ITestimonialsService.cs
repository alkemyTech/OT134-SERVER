﻿using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsService
    {
        IEnumerable<Testimonials> GetAll();
        Testimonials GetById();
        Task<Result> Insert(TestimonialDTO testimonialDTO);
        void Update(Testimonials testimonials);
        Task<Result> Delete(int id);
    }
}