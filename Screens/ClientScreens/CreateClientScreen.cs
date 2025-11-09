using System.Globalization;
using System.Linq.Expressions;
using BankSystem.Models;
using BankSystem.Repositories;

namespace BankSystem.Screens.ClientScreens
{
    public static class CreateClientScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine();
            Console.WriteLine("Novo Cliente do Banco");
            Console.WriteLine("----------------------");
            Console.WriteLine("Nome: ");
            var name = Console.ReadLine();

            Console.WriteLine("Documento: ");
            var document = Console.ReadLine();

            Console.WriteLine("Telefone: ");
            var telephone = Console.ReadLine();

            Console.WriteLine("Email: ");
            var email = Console.ReadLine();

            Console.WriteLine("Endereço: ");
            var adress = Console.ReadLine();

            Console.WriteLine("Data de nascimento: (dd/mm/aaaa)");
            DateOnly bornDate;
            bool dataValida = false;
            do
            {
                string input = Console.ReadLine();
                dataValida = DateOnly.TryParse(input, new CultureInfo("pt-BR"), out bornDate);
                if (!dataValida)
                {
                    Console.WriteLine("Formato Invalido de data");
                }
            } while (!dataValida);

            Create(new Clientes
            {
                Nome = name,
                Documento = document,
                Telefone = telephone,
                Email = email,
                Endereco = adress,
                DataNascimento = bornDate
            });
            Console.ReadKey();
            MenuClientScreen.Load();
        }

        public static void Create(Clientes clientes)
        {
            try
            {
                var repository = new Repository<Clientes>(Database.Connection);
                repository.Create(clientes);
                Console.WriteLine("Cliente cadastrado no banco");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel cadastrar o cliente");
                Console.WriteLine(ex.Message);
            }
        }
    }
}