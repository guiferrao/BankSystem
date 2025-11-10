using BankSystem.Repositories;
using BankSystem.Models;

namespace BankSystem.Screens.TransactionsScreens
{
    public static class TransferenciaScreen
    {
        public static void Load(int contaOrigemId)
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine();
            Console.WriteLine("Para qual agência deseja transferir: ");
            var agenciaDestino = Console.ReadLine();

            Console.WriteLine("Para qual conta: ");
            var numeroContaDestino = Console.ReadLine();

            int contaDestinoId = 0;
            var repoConta = new ContaRepository(Database.Connection);

            try
            {
                var contaDestino = repoConta.GetByAgenciaeNumero(agenciaDestino, numeroContaDestino);
                if (contaDestino == null)
                {
                    Console.WriteLine("Conta de destino não encontrada");
                }
                else if (contaDestino.Id == contaOrigemId)
                {
                    Console.WriteLine("Não é possível transferir para a conta de origem");
                }
                else
                {
                    contaDestinoId = contaDestino.Id;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro ao buscar conta de destino");
            }

            if (contaDestinoId == 0)
            {
                Console.ReadLine();
                MenuTransactionsScreen.Load(contaOrigemId);
                return;
            }

            Console.WriteLine("Digite o valor a ser transferido: ");

            if(!decimal.TryParse(Console.ReadLine(), out decimal valor) || valor <= 0)
            {
                Console.WriteLine("Valor invalido");
                Console.ReadKey();
                MenuTransactionsScreen.Load(contaOrigemId);
                return;
            }
            try
            {
                bool sucesso = repoConta.Transferir(contaOrigemId, contaDestinoId, valor);
                if (sucesso)
                {
                    Console.WriteLine("Transferencia realizada com sucesso");
                }
                else
                {
                    Console.WriteLine("Saldo insuficiente para realizar transferencia");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro ao realizar transferencia");
            }

            Console.ReadKey();
            MenuTransactionsScreen.Load(contaOrigemId);
        }
    }
}