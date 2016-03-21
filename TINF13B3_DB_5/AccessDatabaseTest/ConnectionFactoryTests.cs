using System;
using System.Data;
using System.Data.OleDb;
using AccessDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessDatabaseTest
{
    [TestClass]
    public class ConnectionFactoryTests
    {
        [TestMethod]
        public void Get_NoParameter_ReturnsExpectedOleDBConnection()
        {
            OleDbConnection actual = ConnectionFactory.Get();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Get_NoParameter_WithExpectedConnectionString()
        {
            var expected = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Daten\Database\DHBW\TINF13B3.mdb";

            var actual = ConnectionFactory.Get();

            Assert.AreEqual(expected, actual.ConnectionString);
        }

        [TestMethod]
        public void Get_NoParameter_ConnectionStateIsClosed()
        {
            var actual = ConnectionFactory.Get();

            Assert.IsTrue(actual.State == ConnectionState.Closed);
        }

        [TestMethod]
        public void Get_CallTwice_ReturnsDifferentConnections()
        {
            var connection1 = ConnectionFactory.Get();
            var connection2 = ConnectionFactory.Get();

            Assert.AreNotEqual(connection1, connection2);
            Assert.IsFalse(connection1 == connection2);
        }
    }
}