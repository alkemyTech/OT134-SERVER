using Microsoft.AspNetCore.Http;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IActivitiesService
    {
        IEnumerable<Activities> GetAll();
        Activities GetById();
        Task<Result> Insert(ActivityDTOForRegister activities);
        void Update(Activities activities);
        void Delete(Activities activities);
    }
}