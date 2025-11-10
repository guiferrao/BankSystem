using BankSystem.Models;
using BankSystem.Repositories;
using BankSystem.Screens.ClientScreens;
using BankSystem.Screens.TransactionsScreens;

namespace BankSystem.Screens.AccountScreens
{
    public static class AccountMenuScreens
    {
        public static void Load(int clienteId)
        {
            Console.Clear();
            
            var repository = new Repository<Clientes>(Database.Connection);
            var cliente = repository.Get(clienteId);
            Console.WriteLine($"Bem vindo! {cliente.Nome}");

            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine("Painel do Cliente");
            Console.WriteLine();

            var repositoryContas = new ContaRepository(Database.Connection);
            var contas = repositoryContas.GetByClienteId(clienteId);

            Console.WriteLine("Suas contas: ");
            foreach (var conta in contas)
            {
                Console.WriteLine($" - Conta {conta.TipoConta}: {conta.Agencia}-{conta.NumeroConta}");
            }

            Console.WriteLine();
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine("1 - Criar nova conta");
            Console.WriteLine("2 - Acessar uma conta");
            Console.WriteLine();
            Console.WriteLine("Aperte ESC para voltar");

            var option = Console.ReadKey();

            switch (option.Key)
            {
                case ConsoleKey.D1:
                    CreateAccountScreen.Load(clienteId);
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Digite o numero da agencia que deseja acessar: ");
                    var numeroAgencia = Console.ReadLine();
                    Console.WriteLine("Digite o numero da conta que deseja acessar: ");
                    var numeroConta = Console.ReadLine();
                    var repoContas = new ContaRepository(Database.Connection);
                    var conta = repoContas.GetByAgenciaeNumero(numeroAgencia, numeroConta);
                    if (conta != null)
                    {
                        MenuTransactionsScreen.Load(conta.Id);
                    }
                    else
                    {
                        Console.WriteLine("Agência ou número da conta não encontrados");
                        Console.WriteLine("Pressione qualquer tecla para voltar");
                        Console.ReadKey();
                        Load(clienteId);
                    }
                    break;
                case ConsoleKey.Escape:
                    MenuClientScreen.Load();
                    break;
                default: Load(clienteId); break;
            }
        }
    }
}