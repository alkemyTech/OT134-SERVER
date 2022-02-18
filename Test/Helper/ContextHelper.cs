
using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;

namespace Test.Helper
{
    public class ContextHelper
    {
        public static AppDbContext DbContext { get; set; }
        public static IUnitOfWork UnitOfWork;
        public static IEntityMapper EntityMapper;
        public static IConfiguration Config;
        public static IJwtHelper JwtHelper;
        
        public static void MakeContext()
        {   
            EntityMapper = new EntityMapper();
            Config = new PrepareConfigurationHelper().Config;
            JwtHelper = new JwtHelper(Config);
        }

        public static void MakeDbContext(bool pupulate=true)
        {
            DbContext = PrepareDbContextHelper.MakeDbContext(pupulate);
            UnitOfWork = new PrepareUnitOfWorkHelper(DbContext).unitOfWork;            
        }
    }
}
