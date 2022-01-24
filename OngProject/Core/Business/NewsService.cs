using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<New> GetAll()
        {
            throw new NotImplementedException();
        }

        public New GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(New news)
        {
            throw new NotImplementedException();
        }

        public void Update(New news)
        {
            throw new NotImplementedException();
        }

        public void Delete(New news)
        {
            throw new NotImplementedException();
        }
    }
}
