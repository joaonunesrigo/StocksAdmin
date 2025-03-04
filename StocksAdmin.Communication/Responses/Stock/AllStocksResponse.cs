namespace StocksAdmin.Communication.Responses.Stocks
{
    public record AllStocksResponse
    {
        public List<StockResponse> Stocks { get; set; } = [];
    }
}
