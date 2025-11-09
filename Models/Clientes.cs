using Dapper.Contrib.Extensions;

namespace BankSystem.Models
{
    [Table("[Clientes]")]
    public class Clientes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public DateOnly DataNascimento { get; set; }
    }
}