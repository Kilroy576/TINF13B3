using AccessDatabase.Entity;
using AccessDatabase.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessDatabaseTest.Entity
{
    [TestClass]
    public class DojosTests
    {
        [TestMethod]
        public void Ctor_NewInstance_ImplementsIEntity()
        {
            var target = new Dojos();

            Assert.IsNotNull(target as IEntity);
        }
    }
}
