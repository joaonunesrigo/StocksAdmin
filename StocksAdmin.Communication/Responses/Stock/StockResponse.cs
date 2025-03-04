namespace StocksAdmin.Communication.Responses.Stocks
{
    public record StockResponse
    {
        public long Id { get; set; }

        public required string Nome { get; set; }
    }
}
