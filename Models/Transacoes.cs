using Dapper.Contrib.Extensions;

namespace BankSystem.Models
{
    [Table("[Transacoes]")]
    public class Transacoes
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public decimal Valor { get; set; }
        public DateOnly DataTransacao { get; set; }
        public int TipoTransacao{ get; set; }
    }
}