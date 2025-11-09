using Dapper.Contrib.Extensions;

namespace BankSystem.Models
{
    [Table("[Tipostransacao]")]
    public class TiposTransacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}