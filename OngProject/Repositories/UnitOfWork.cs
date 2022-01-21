using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        // Context
        private readonly DbContext _dbContext;

        // Repositories
        private IActivitiesRepository _activitiesRepository;

        public UnitOfWork(DbContext dbContext, IActivitiesRepository activitiesRepository)
        {
            _dbContext = dbContext;
            _activitiesRepository = activitiesRepository;
        }

        public void Save()
        {
        }
    }
}
