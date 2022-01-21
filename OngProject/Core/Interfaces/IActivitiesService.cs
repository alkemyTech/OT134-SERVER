using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    interface IActivitiesService
    {
        public IEnumerable<Activities> GetAll();
        public Activities GetById();
        public void Insert(Activities activities);
        public void Update(Activities activities);
        public void Delete(Activities activities);
    }
}
