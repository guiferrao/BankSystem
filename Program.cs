using BankSystem.Screens.ClientScreens;
using Microsoft.Data.SqlClient;
using Dapper;
using BankSystem.Data;

namespace BankSystem
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost, 1433;Database=bank-system;User ID=sa;Password=Password123;TrustServerCertificate=True";
        static void Main()
        {
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            Database.Connection = new SqlConnection(CONNECTION_STRING);

            Load();

        }

        private static void Load()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("------------------");
                Console.WriteLine("|  Banco CSharp  |");
                Console.WriteLine("------------------");
                Console.WriteLine();
                Console.WriteLine("O que deseja fazer?");
                Console.WriteLine();
                Console.WriteLine("1 - Gerenciar Clientes");
                Console.WriteLine();
                Console.WriteLine("Aperte ESC para sair");
                Console.WriteLine();

                var option = Console.ReadKey();

                switch (option.Key)
                {
                    case ConsoleKey.D1:
                        MenuClientScreen.Load();
                        break;
                    case ConsoleKey.Escape:
                        running = false;
                        break;
                    default: Load(); break;
                }
            }
        }
    }
}
