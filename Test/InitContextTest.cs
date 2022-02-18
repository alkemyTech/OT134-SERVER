using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{   
    [TestClass]
    public class InitContextTest
    {
        /// <summary>
        ///     Inicializa contexto general para todos los tests
        /// </summary>
        /// <param name="testContext"></param>
        [AssemblyInitialize()]
        public static void Setup(TestContext testContext)
        {
            ContextHelper.MakeDbContext(); // inicializa db, unitofwork
            ContextHelper.MakeContext();
        }
    }
}
