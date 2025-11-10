using BankSystem.Models;
using BankSystem.Repositories;

namespace BankSystem.Screens.TransactionsScreens
{
    public static class SaqueScreen
    {
        public static void Load(int contaId)
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine();
            Console.WriteLine("Qual o valor do saque?");
            var valor = decimal.Parse(Console.ReadLine());
    
            if (valor <= 0)
            {
                Console.WriteLine("Não é possível realizar o saque");
                Console.ReadKey();
                SaqueScreen.Load(contaId);
            }
            try
            {
                var repoConta = new ContaRepository(Database.Connection);
                bool sucesso = repoConta.Sacar(contaId, valor);
                if (sucesso)
                {
                    Console.WriteLine("Saque realizado com sucesso");
                }
                else
                {
                    Console.WriteLine("Impossível realizar saque, saldo insuficiente");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Não foi possível realizar saque");
            }

            Console.ReadKey();
            SaqueScreen.Load(contaId);
        }
    }
}