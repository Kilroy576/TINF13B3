using System.Collections.Generic;
using AccessDatabase.Entity;
using AccessDatabase.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessDatabaseTest.Repository
{
    [TestClass]
    public class DojosRepositoryTests
    {
        private DojosRepository _target;
        private Dojos _TestDojo;

        [TestInitialize]
        public void SetUp()
        {
            _target = new DojosRepository();
            _TestDojo = new Dojos
            {
                intIDPersonen = 1,
                strDojoName = "Test DojoName",
                intDDLinkStatus = 50,
                intDDStilrichtung = 0, //93,
                intDDBundesland = 0,
                intDDLand = 0,
                intIDLastChanged = 1,
                intIDErstVon = 1
            };
        }

        [TestMethod]
        public void Ctor_NewInstance_ImplementsBaseRepository()
        {

            Assert.IsNotNull(_target as BaseRepository);
        }

        [TestMethod]
        public void Get_NoParameter_ReturnsAllDojos()
        {
            List<Dojos> actual = _target.Get();

            Assert.AreEqual(1526, actual.Count);
        }

        [TestMethod]
        public void Get_ParameterID590_ReturnsOneDojos()
        {
            int id = 590;

            Dojos actual = _target.Get(id);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Get_ParameterID590_IsBudoClubKarlsruhe()
        {
            int id = 590;
            string expected = "Budo-Club Karlsruhe e.V.";

            Dojos actual = _target.Get(id);

            Assert.AreEqual(expected, actual.strDojoName);
        }

        [TestMethod]
        public void Insert_NewDojo_DojoWasInserted()
        {
            string expected = "Test DojoName";


            Dojos actual = _target.Insert(_TestDojo);

            Assert.AreEqual(expected, actual.strDojoName);

            _target.Delete(_TestDojo.IDDojos);
        }

        [TestMethod]
        public void Update_InsertAndUpdate_WasUpdatedInDatabase()
        {
            var expected        = "Test DojoName Update";
            var dojo            = _target.Insert(_TestDojo);
            dojo.strDojoName    = "Test DojoName Update";
            dojo.strOrt         = "Test Ort Update";

            Dojos updated = _target.Update(dojo);

            var actual = _target.Get(updated.IDDojos);
            Assert.AreEqual(expected, actual.strDojoName);
        }

        [TestMethod, Ignore]
        public void Delete_ParameterID_WasDeleted()
        {
            int id = 1563;
            var actual = _target.Delete(id);

            Assert.AreEqual(1, actual);
        }
    }
}
