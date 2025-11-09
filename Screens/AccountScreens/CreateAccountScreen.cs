using BankSystem.Models;
using BankSystem.Repositories;

namespace BankSystem.Screens.AccountScreens
{
    public static class CreateAccountScreen
    {
        public static void Load(int clienteId)
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine();
            Console.WriteLine("Crie sua conta no Banco");
            Console.WriteLine("----------------------");
            Console.WriteLine("Agencia: (ex: 0001) ");
            var agency = Console.ReadLine();

            Console.WriteLine("Numero da conta: (ex: 12345-6)");
            var numAccount = Console.ReadLine();

            Console.WriteLine("Tipo da conta (1=Corrente, 2=Poupança): ");
            var accountTypeInput = short.Parse(Console.ReadLine());
            var accountType = (accountTypeInput == 1) ? "Corrente" : "Poupança";

            var account = new Contas
            {
                ClienteId = clienteId,
                Agencia = agency,
                NumeroConta = numAccount,
                TipoConta = accountTypeInput,
                Saldo = 0
            };

            try
            {
                var repository = new Repository<Contas>(Database.Connection);
                repository.Create(account);
                Console.WriteLine("Conta criada com sucesso");
            }
            catch (Exception)
            {
                Console.WriteLine("Não foi possivel criar a conta");
            }

            Console.ReadKey();
            AccountMenuScreens.Load(clienteId);
        }
    }
}
