using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Interfaces
{
    public interface INewsService
    {
        IEnumerable<New> GetAll();
        New GetById();
        void Insert(New news);
        void Update(New news);
        void Delete(New news);
    }
}
