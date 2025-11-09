using System.Data;
using System.Linq.Expressions;
using BankSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BankSystem.Repositories
{
    public class ContaRepository
    {
        private readonly string _connectionString;

        public ContaRepository(string connectionString)
            => _connectionString = connectionString;

        private IDbConnection GetConnection()
            => new SqlConnection(_connectionString);

        public decimal VerSaldo(int contaId)
        {
            var query = @"
                SELECT
                    Saldo
                FROM
                    Contas
                WHERE
                    Id = @IdDaConta";

            using (var connection = GetConnection())
            {
                return connection.QuerySingle<decimal>(query, new { IdDaConta = contaId });
            }
        }

        public IEnumerable<Transacoes> ListarExtrato(int contaId)
        {
            var query = @"
                SELECT
                    *
                FROM
                    Saldo
                WHERE
                    ContaId = @IdDaConta
                ORDER BY
                    DataTransacao
                DESC";

            using (var connection = GetConnection())
            {
                return connection.Query<Transacoes>(query, new { IdDaConta = contaId });
            }
        }

        public void Depositar(int contaId, decimal valor)
        {
            var queryUpdate = @"
                UPDATE
                    Contas
                SET
                    Saldo = Saldo + @Valor
                WHERE
                    Id = @ContaId";

            var queryInsert = @"
                INSERT INTO
                    Transacoes 
                    (ContaId, Valor, DataTransacao, TipoTransacao)
                VALUES
                    (@IdDaConta, @Valor, GETDATE(), 1)";

            using (var connection = GetConnection())
            {
                var deposito = new { IdDaConta = contaId, Valor = valor };
                connection.Execute(queryUpdate, deposito);
                connection.Execute(queryInsert, deposito);
            }
        }

        public bool Sacar(int contaId, decimal valor)
        {
            var queryUpdate = @"
                UPDATE 
                    Contas
                SET 
                    Saldo = Saldo - @Valor
                WHERE 
                    Id = @IdDaConta AND Saldo >= @Valor";

            var queryInsert = @"
                INSERT INTO 
                    Transacoes 
                    (ContaId, Valor, DataTransacao, TipoTransacao)
                VALUES 
                    (@IdDaConta, @Valor, GETDATE(), 2)";

            using (var connection = GetConnection())
            {
                var saque = new { IdDaConta = contaId, Valor = valor };

                int linhasAfetadas = connection.Execute(queryUpdate, saque);

                if (linhasAfetadas > 0)
                {
                    connection.Execute(queryInsert, saque);
                    return true;
                }
                else
                {
                    Console.WriteLine("saldo insuficiente");
                    return false;
                }
            }
        }
        
        public bool Transferir(int contaOrigemId, int contaDestinoId, decimal valor)
        {
            var querySaque = @"
                UPDATE 
                    Contas 
                SET 
                    Saldo = Saldo - @Valor 
                WHERE 
                    Id = @IdOrigem AND Saldo >= @Valor";

            var queryDeposito = @"
                UPDATE 
                    Contas 
                SET 
                    Saldo = Saldo + @Valor 
                WHERE 
                    Id = @IdDestino";

            var queryInsertSaida = @"
                INSERT INTO 
                    Transacoes 
                    (ContaId, Valor, TipoTransacao, DataTransacao) 
                VALUES 
                    (@IdOrigem, @Valor, 3, GETDATE())";

            var queryInsertEntrada = @"
                INSERT INTO 
                    Transacoes 
                    (ContaId, Valor, TipoTransacao, DataTransacao) 
                VALUES 
                    (@IdDestino, @Valor, 4, GETDATE())";

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var transSaque = new { IdOrigem = contaOrigemId, Valor = valor };
                        int linhasAfetadas = connection.Execute(querySaque, transSaque, transaction: transaction);

                        if (linhasAfetadas == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        var transDeposito = new { IdDestino = contaDestinoId, Valor = valor };
                        connection.Execute(queryDeposito, transDeposito, transaction: transaction);

                        connection.Execute(queryInsertSaida, transSaque, transaction: transaction);
                        connection.Execute(queryInsertEntrada, transDeposito, transaction: transaction);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}