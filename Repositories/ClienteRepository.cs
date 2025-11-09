using System.Data;
using BankSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace BankSystem.Repositories
{
    public class ClienteRepository : Repository<Clientes>
    {
        private readonly SqlConnection _connection;

        public ClienteRepository(SqlConnection connection) : base(connection)
            => _connection = connection;

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

            try
            {
                _connection.Open();
                return _connection.Query<Clientes>(query, clientes);
            }
            finally
            {
                _connection.Close();
            }
        }
        public Clientes GetByDocumento(string documento)
        {
            var query = @"
                SELECT
                    *
                FROM
                    Clientes
                WHERE
                    Documento = @Documento";

            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<Clientes>(query, new { Documento = documento });
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}