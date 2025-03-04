using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StocksAdmin.Api.Entities
{
    [Table("stocks")]
    public class Stock
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("wallet_id")]
        public long WalletId { get; set; }

        [Column("codigo")]
        public required string Codigo { get; set; }

        [Column("nome")]
        public required string Nome { get; set; }

        [Column("quantidade")]
        public int Quantidade { get; set; }

        [Column("preco_atual")]
        public double CurrentPrice { get; set; }

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; } = null!;
    }
}
