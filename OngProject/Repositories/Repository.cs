using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace OngProject.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected AppDbContext RepositoryContext { get; set; }
        protected DbSet<T> dbSet;
        public Repository(AppDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
            dbSet = repositoryContext.Set<T>();
        }
        public async Task<ICollection<T>> FindAllAsync()
        {
            return await dbSet.ToListAsync();
        }
        public async Task<ICollection<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.Where(expression).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
