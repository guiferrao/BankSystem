using System.ComponentModel;
using BankSystem.Models;
using BankSystem.Repositories;
using BankSystem.Screens.AccountScreens;
using Microsoft.Identity.Client;

namespace BankSystem.Screens.TransactionsScreens
{
    public static class MenuTransactionsScreen
    {
        public static void Load(int idDaConta)
        {
            Console.Clear();

            var repoConta = new Repository<Contas>(Database.Connection);
            var conta = repoConta.Get(idDaConta);

            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine($"Conta: {conta.Agencia}/{conta.NumeroConta}");
            Console.WriteLine($"Saldo em conta {conta.Saldo:C}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine("1 - Realizar depósito");
            Console.WriteLine("2 - Realizar saque");
            Console.WriteLine("3 - Realizar transferência");
            Console.WriteLine("4 - Ver extrato");
            Console.WriteLine();
            Console.WriteLine("Aperte ESC para voltar");

            var option = Console.ReadKey();

            switch (option.Key)
            {
                case ConsoleKey.D1:
                    DepositoScreen.Load(idDaConta);
                    break;
                case ConsoleKey.D2:
                    SaqueScreen.Load(idDaConta);
                    break;
                case ConsoleKey.D3:
                    TransferenciaScreen.Load(idDaConta);
                    break;
                case ConsoleKey.D4:
                    ExtratoScreen.Load(idDaConta);
                    break;
                case ConsoleKey.Escape:
                    AccountMenuScreens.Load(conta.ClienteId);
                    break;
                default: Load(idDaConta); break;
            }
        }
    }
}