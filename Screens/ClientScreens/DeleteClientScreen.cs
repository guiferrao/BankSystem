using System.Globalization;
using System.Linq.Expressions;
using BankSystem.Models;
using BankSystem.Repositories;

namespace BankSystem.Screens.ClientScreens
{
    public static class DeleteClientScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine();
            Console.WriteLine("Excluir cliente do Banco");
            Console.WriteLine("----------------------");
            Console.WriteLine("Qual Id do cliente a excluir: ");
            var id = Console.ReadLine();

            Delete(int.Parse(id));
            
            Console.ReadKey();
            MenuClientScreen.Load();
        }

        public static void Delete(int id)
        {
            try
            {
                var repository = new Repository<Clientes>(Database.Connection);
                repository.Delete(id);
                Console.WriteLine("Cliente excluido do banco");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel excluir o cliente");
                Console.WriteLine(ex.Message);
            }
        }
    }
}