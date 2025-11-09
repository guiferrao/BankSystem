using Dapper.Contrib.Extensions;

namespace BankSystem.Models
{
    [Table("[TiposConta]")]
    public class TiposConta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}