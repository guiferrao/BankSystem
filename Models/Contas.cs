using Dapper.Contrib.Extensions;

namespace BankSystem.Models
{
    [Table("[Contas]")]
    public class Contas
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public decimal Saldo { get; set; }
        public int TipoConta{ get; set; }
    }
}