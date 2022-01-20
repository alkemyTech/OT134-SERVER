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

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
        }
    }
}
