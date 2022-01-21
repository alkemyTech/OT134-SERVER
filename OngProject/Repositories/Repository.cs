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
        protected DbContext RepositoryContext { get; set; }
        public Repository(DbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
            dbSet = repositoryContext.Set<T>();
        }
        public IQueryable<T> FindAll()
        {
            return dbSet.AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
        }
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
