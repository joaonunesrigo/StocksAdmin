using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StocksAdmin.Api.Entities
{
    [Table("wallets")]
    public class Wallet
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("nome")]
        public required string Name { get; set; }

        List<Stock> Stocks { get; set; } = [];
    }
}
