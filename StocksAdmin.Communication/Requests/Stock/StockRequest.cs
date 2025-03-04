using System.ComponentModel.DataAnnotations;

namespace ProjectClientHub.Communication.Requests.Stock
{
    public record StockRequest
    {
        [Required(ErrorMessage = "Informe o Id da carteira que o ativo irá pertencer.")]
        public long WalletId { get; set; }

        [Required(ErrorMessage = "O código do ativo é obrigatório.")]
        public required string Codigo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

    }
}
