using AccessDatabase.Entity;
using AccessDatabase.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessDatabaseTest.Entity
{
    [TestClass]
    public class TermineTests
    {
        [TestMethod]
        public void Ctor_NewInstance_ImplementsIEntity()
        {
            var target = new Termine();

            Assert.IsNotNull(target as IEntity);
        }
    }
}
