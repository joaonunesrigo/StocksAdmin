using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StocksAdmin.Api.Entities
{
    [Table("users")]
    public record User
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = "";

        [Column("password")]
        public string Password { get; set; } = "";

        [Column("email")]
        public string Email { get; set; } = "";
    }
}
