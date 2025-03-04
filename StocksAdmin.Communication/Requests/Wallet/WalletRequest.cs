using System.ComponentModel.DataAnnotations;

namespace ProjectClientHub.Communication.Requests.Wallet
{
    public record WalletRequest
    {
        [Required(ErrorMessage = "O nome da carteira é obrigatório.")]
        public required string Nome { get; set; }
    }
}
