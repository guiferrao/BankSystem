using System.Globalization;
using BankSystem.Repositories;

namespace BankSystem.Screens.TransactionsScreens
{
    public static class ExtratoScreen
    {
        public static void Load(int contaId)
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("|  Banco CSharp  |");
            Console.WriteLine("------------------");
            Console.WriteLine("Extrato da conta");
            Console.WriteLine("------------------");
            var repoConta = new ContaRepository(Database.Connection);
            var transacoes = repoConta.ListarExtrato(contaId);

            Console.WriteLine();
            Console.WriteLine(String.Format("{0,-20} | {1,-20} | {2,15}", "Data", "Tipo de Transação", "Valor"));
            Console.WriteLine(new string('-', 59));

            foreach (var transacao in transacoes)
            {
                string tipoTransacaoTexto;
                switch (transacao.TipoTransacao)
                {
                    case 1:
                        tipoTransacaoTexto = "Depósito";
                        break;
                    case 2:
                        tipoTransacaoTexto = "Saque";
                        break;
                    case 3:
                        tipoTransacaoTexto = "Transferência (Saída)";
                        break;
                    case 4:
                        tipoTransacaoTexto = "Transferência (Entrada)";
                        break;
                    default:
                        tipoTransacaoTexto = "Desconhecida";
                        break;
                }
                string valorFormatado = transacao.Valor.ToString("C", new CultureInfo("pt-BR"));

                Console.WriteLine(String.Format("{0,-20} | {1,-20} | {2,15}",
                    transacao.DataTransacao,
                    tipoTransacaoTexto,
                    valorFormatado
                ));
            }
            
            Console.WriteLine(new string('-', 59));
            Console.ReadKey();
            MenuTransactionsScreen.Load(contaId);
        }
    }
}