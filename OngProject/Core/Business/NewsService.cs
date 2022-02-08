using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;

        public NewsService(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
