using System.Data.OleDb;

namespace AccessDatabase
{
    public class ConnectionFactory
    {
        public static OleDbConnection Get()
        {
            // Place to add some logic to return different Connections
            // ...
            // Set connectionstring or take it from settings ... or somewhere else
            var connectionString    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Daten\Database\DHBW\TINF13B3.mdb";
            var connection          = new OleDbConnection(connectionString);
            return connection;
        }
    }
}