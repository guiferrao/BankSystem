using System.Data;
using Microsoft.Data.SqlClient;

namespace BankSystem
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost,1433;Database=bank-system;User ID=sa;Password=Password123;TrustServerCertificate=True";
        static void Main()
        {
            Database.Connection = new SqlConnection(CONNECTION_STRING);
            Database.Connection.Open();

            Console.ReadKey();
            Database.Connection.Close();
        }
    }
}
