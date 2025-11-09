using System.Data;
using BankSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BankSystem.Repositories
{
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString)
            => _connectionString = connectionString;

        private IDbConnection GetConnection()
            => new SqlConnection(_connectionString);

        public IEnumerable<Clientes> BuscarPorNome(string nomeParcial)
        {
            var query = @"
                SELECT
                    *
                FROM
                    Clientes
                WHERE
                    Nome
                LIKE
                    @NomeBuscado";

            var clientes = new { NomeBuscado = "%" + nomeParcial + "%" };

            using (var connection = GetConnection())
            {
                return connection.Query<Clientes>(query, clientes);
            }
        }
    }
}