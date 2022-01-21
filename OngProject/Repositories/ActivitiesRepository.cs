using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class ActivitiesRepository : Repository<Activities>, IActivitiesRepository
    {
        public ActivitiesRepository(DbContext dbContext) : base(dbContext) { }

        public IEnumerable<Activities> GetAll()
        {
            throw new NotImplementedException();
        }

        public Activities GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(Activities activities)
        {
            throw new NotImplementedException();
        }

        public override void Update(Activities activities)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Activities activities)
        {
            throw new NotImplementedException();
        }
    }
}
