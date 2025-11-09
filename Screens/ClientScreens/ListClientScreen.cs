using BankSystem.Models;
using BankSystem.Repositories;

namespace BankSystem.Screens.ClientScreens
{
    public static class ListClientScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine();
            Console.WriteLine("Lista de clientes do banco");
            Console.WriteLine("--------------------------");
            List();
            Console.ReadKey();
            MenuClientScreen.Load();
        }

        public static void List()
        {
            var repository = new Repository<Clientes>(Database.Connection);
            var clientes = repository.Get();
            foreach (var cliente in clientes)
                Console.WriteLine($"{cliente.Id} - {cliente.Nome} ({cliente.DataNascimento})");
        }
    }
}