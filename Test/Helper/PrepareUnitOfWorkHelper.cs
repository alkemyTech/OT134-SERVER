
using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

namespace Test.Helper
{
    internal class PrepareUnitOfWorkHelper
    {
        public readonly IUnitOfWork unitOfWork;
        public PrepareUnitOfWorkHelper(AppDbContext dbContext)
        {
            unitOfWork = new UnitOfWork(dbContext);
        }
    }
}
