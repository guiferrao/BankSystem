using System.Data;
using System.Linq.Expressions;
using BankSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BankSystem.Repositories
{
    public class ContaRepository : Repository<Contas>
    {
        private readonly SqlConnection _connection;

        public ContaRepository(SqlConnection connection) : base(connection)
            => _connection = connection;

        public decimal VerSaldo(int contaId)
        {
            var query = @"
                SELECT
                    Saldo
                FROM
                    Contas
                WHERE
                    Id = @IdDaConta";

            try
            {
                _connection.Open();
                return _connection.QuerySingle<decimal>(query, new { IdDaConta = contaId });
            }
            finally
            {
                _connection.Close();
            }
        }

        public IEnumerable<Transacoes> ListarExtrato(int contaId)
        {
            var query = @"
                SELECT
                    *
                FROM
                    Transacoes
                WHERE
                    ContaId = @IdDaConta
                ORDER BY
                    DataTransacao
                DESC";

            try
            {
                _connection.Open();
                return _connection.Query<Transacoes>(query, new { IdDaConta = contaId });
            }
            finally
            {
                _connection.Close();
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
                    (@ContaId, @Valor, GETDATE(), 1)";

            try
            {
                _connection.Open();
                var deposito = new { ContaId = contaId, Valor = valor };
                _connection.Execute(queryUpdate, deposito);
                _connection.Execute(queryInsert, deposito);
            }
            finally
            {
                _connection.Close();
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

            try
            {
                _connection.Open();
                var saque = new { IdDaConta = contaId, Valor = valor };

                int linhasAfetadas = _connection.Execute(queryUpdate, saque);

                if (linhasAfetadas > 0)
                {
                    _connection.Execute(queryInsert, saque);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                _connection.Close();
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

            try
            {
                _connection.Open();

                using (var transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        var transSaque = new { IdOrigem = contaOrigemId, Valor = valor };
                        int linhasAfetadas = _connection.Execute(querySaque, transSaque, transaction: transaction);

                        if (linhasAfetadas == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        var transDeposito = new { IdDestino = contaDestinoId, Valor = valor };
                        _connection.Execute(queryDeposito, transDeposito, transaction: transaction);

                        _connection.Execute(queryInsertSaida, transSaque, transaction: transaction);
                        _connection.Execute(queryInsertEntrada, transDeposito, transaction: transaction);

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
            finally
            {
                _connection.Close();
            }
        }

        public IEnumerable<Contas> GetByClienteId(int clienteId)
        {
            var query = @"
                SELECT
                    *
                FROM
                    Contas
                WHERE
                    ClienteId = @ClienteId";

            var clientesId = new { ClienteId = clienteId };

            try
            {
                _connection.Open();
                return _connection.Query<Contas>(query, clientesId);
            }
            finally
            {
                _connection.Close();
            }
        }
        
        public Contas GetByAgenciaeNumero(string agencia, string numero)
        {
            var query = @"
                SELECT
                    *
                FROM
                    Contas
                WHERE
                    Agencia = @Agencia
                AND
                    NumeroConta = @Numero";

            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<Contas>(query, new { Agencia = agencia, Numero = numero });
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}