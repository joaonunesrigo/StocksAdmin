using System.ComponentModel.DataAnnotations;

namespace ProjectClientHub.Communication.Requests.Stock
{
    public record UpdateStockRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }
    }
}
