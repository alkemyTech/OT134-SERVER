using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsService
    {
        public IEnumerable<New> GetAll();
        public New GetById();
        public void Insert(New news);
        public void Update(New news);
        public void Delete(New news);
    }
}
