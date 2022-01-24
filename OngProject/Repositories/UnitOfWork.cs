using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructor and Context
        private readonly DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Repositories
        private readonly IRepository<Activities> _activitiesRepository;
        private readonly IRepository<New> _newsRepository;
        private readonly IRepository<Testimonials> _testimonialsRepository;

        public IRepository<Activities> ActivitiesRepository => _activitiesRepository ?? new Repository<Activities>(_dbContext);
        public IRepository<New> NewsRepository => NewsRepository ?? new Repository<New>(_dbContext);
        public IRepository<Testimonials> TestimonialsRepository => TestimonialsRepository ?? new Repository<Testimonials>(_dbContext);
        private readonly IRepository<User> _userrepository;

        public IRepository<Activities> ActivitiesRepository => _activitiesRepository ?? new Repository<Activities>(_dbContext);
        public IRepository<New> NewsRepository => NewsRepository ?? new Repository<New>(_dbContext);
        public IRepository<User> UserRepository => UserRepository ?? new Repository<User>(_dbContext);
        #endregion

        #region Methods
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
