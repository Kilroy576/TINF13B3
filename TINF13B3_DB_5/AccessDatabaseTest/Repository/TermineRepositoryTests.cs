using System;
using System.Collections.Generic;
using AccessDatabase.Entity;
using AccessDatabase.Interface;
using AccessDatabase.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessDatabaseTest.Repository
{
    [TestClass]
    public class TermineRepositoryTests
    {
        private TermineRepository _target;
        private Termine _testTermin;

        [TestInitialize]
        public void SetUp()
        {
            _target     = new TermineRepository();
            _testTermin = new Termine
            {
                strOrt = "Test Ort",
                intIDPersonen = 1,
                dtmDatumvon = new DateTime(2016, 4, 1),
                dtmDatumbis = new DateTime(2016, 4, 1),
                strTitel = "Test Titel",
                strBeschreibung = "Test Beschreibung",
                strTerminUrl = "",
                intIDDojoAusrichter = 0,
                strVerband = "Test Verband",
                blnLehrgang = false,
                blnMeisterschaft = false,
                blnFreigegeben = true,
                intDDLinkStatus = 0,
                intDDQuelleTermine = 0,
                intDDStatusTermine = 0
            };
        }

        [TestMethod]
        public void Ctor_NewInstance_NotNull()
        {
            Assert.IsNotNull(_target);
        }

        [TestMethod]
        public void Ctor_NewInstance_IMplementsIRepository()
        {
            var actual = _target as IRepository;

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Get_NoParameter_ReturnsAListOfTermine()
        {
            List<Termine> actual = _target.Get();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual,typeof(List<Termine>));
        }

        [TestMethod]
        public void Get_NoParameter_Has6847Items()
        {
            List<Termine> actual = _target.Get();

            Assert.AreEqual(6847, actual.Count);
        }

        [TestMethod]
        public void Get_ParameterID4711_ReturnsOneTremin()
        {
            int id = 4711;

            Termine actual = _target.Get(id);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Get_ParameterID4711_ReturnsOneTerminWithCorretID()
        {
            int id = 4711;
            int expected = 4711;

            Termine actual = _target.Get(id);

            Assert.AreEqual(expected, actual.IDTermine);
        }

        [TestMethod]
        public void Get_ParameterID4711_TerminWithCorretValues()
        {
            int id = 4711;

            Termine actual = _target.Get(id);

            Assert.AreEqual(4711, actual.IDTermine);
            Assert.AreEqual("Calw-Stammheim", actual.strOrt);
            Assert.AreEqual(1, actual.intIDPersonen);
            Assert.AreEqual(new DateTime(2011,09,24), actual.dtmDatumvon);
            Assert.AreEqual(null, actual.dtmDatumbis);
            Assert.AreEqual("Hermann Hesse Cup 2011", actual.strTitel);
            Assert.AreEqual(null, actual.strBeschreibung);
            Assert.AreEqual(@"http://www.skd-calw.de/wp-content/uploads/Ausschreibung_HHC_2011.pdf", actual.strTerminUrl);
            Assert.AreEqual(515, actual.intIDDojoAusrichter);
            Assert.AreEqual("DJKB", actual.strVerband);
            Assert.AreEqual(false, actual.blnLehrgang, "blnLehrgang");
            Assert.AreEqual(true, actual.blnMeisterschaft, "blnMeisterschaft");
            Assert.AreEqual(true, actual.blnFreigegeben, "blnFreigegeben");
            Assert.AreEqual(50, actual.intDDLinkStatus);
            Assert.AreEqual(new DateTime(2011,08,04,08,2,40), actual.dtmLastChanged, "dtmLastChanged");
            Assert.AreEqual(1, actual.intIDLastChanged, "intIDLastChanged");
            Assert.AreEqual(new DateTime(2011, 08, 04, 08, 2, 40), actual.dtmErstAm, "dtmErstAm");
            Assert.AreEqual(1, actual.intIDErstVon, "intIDErstVon");
            Assert.AreEqual(125, actual.intDDQuelleTermine, "intDDQuelleTermine");
            Assert.AreEqual(0, actual.intDDStatusTermine, "intDDStatusTermine");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_ParameterIsNull_ThrowException()
        {
            _target.Insert(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Insert_OldTermineWithIDNot0_ThrowException()
        {
            _testTermin.IDTermine = 99999;
            
            _target.Insert(_testTermin);
        }

        [TestMethod]
        public void Insert_NewTermine_ReturnsSavedTerminWithIDNot0()
        {
            Termine actual = _target.Insert(_testTermin);

            Assert.AreNotEqual(0, actual);
        }


        [TestMethod]
        public void Insert_NewTermine_TerminExistsInDB()
        {
            Termine actual = _target.Insert(_testTermin);

            var termin = _target.Get(actual.IDTermine);

            Assert.IsNotNull(termin);
        }

        [TestMethod]
        public void Delete_IDIs0_NoRowAffected()
        {
            int actual = _target.Delete(0);

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void Delete_InsertAndDelete_NoRowInDatabase()
        {
            Termine actual = _target.Insert(_testTermin);

            var affectedRows = _target.Delete(actual.IDTermine);

            Assert.AreEqual(1,affectedRows);
            var termin = _target.Get(actual.IDTermine);
            Assert.IsNull(termin);
        }


        [TestMethod]
        public void Update_InsertTerminAndUpdate_WasUpdatedInDatabase()
        {
            var expected    = "Test Titel Update";
            var termin      = _target.Insert(_testTermin);
            termin.strTitel = "Test Titel Update";
            termin.strOrt   = "Test Ort Update";

            Termine updated = _target.Update(termin);

            var actual = _target.Get(updated.IDTermine);
            Assert.AreEqual(expected,actual.strTitel);
        }

        [TestMethod, Ignore]
        public void Delete_TestDescription_ExpectedResult()
        {
            _target.Delete(8348);
        }
    }
}