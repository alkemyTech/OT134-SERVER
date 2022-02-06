﻿using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OngProject.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetAll();
        public Task<Result> GetById(int id);
        public void Insert(Category category);
        public void Update(Category category);
        public void Delete(Category category);
    }
}
