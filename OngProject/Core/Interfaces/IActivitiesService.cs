using Microsoft.AspNetCore.Http;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OngProject.Core.Interfaces
{
    public interface IActivitiesService
    {
        IEnumerable<Activities> GetAll();
        Activities GetById();
        Task<Result> Insert(ActivityDTOForRegister activities);
        Task <Result> Update(int id, ActivitiesDtoForUpload activitiesDto);
        void Delete(Activities activities);
    }
}