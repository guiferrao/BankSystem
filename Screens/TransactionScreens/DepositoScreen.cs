using BankSystem.Models;
using BankSystem.Repositories;

namespace BankSystem.Screens.TransactionsScreens
{
    public static class DepositoScreen
    {
        public static void Load(int contaId)
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine();
            Console.WriteLine("Qual o valor do depósito");
            var valor = decimal.Parse(Console.ReadLine());

            if (valor <= 0)
            {
                Console.WriteLine("Valor invalido");
                MenuTransactionsScreen.Load(contaId);
            }
            try
            {
                var repoConta = new ContaRepository(Database.Connection);
                repoConta.Depositar(contaId, valor);
                Console.WriteLine("Depósito realizado com sucesso");
            }
            catch (Exception)
            {
                Console.WriteLine("Não foi possível realizar o depósito");
            }

            Console.ReadKey();
            MenuTransactionsScreen.Load(contaId);
        }
    }
}