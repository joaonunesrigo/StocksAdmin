namespace StocksAdmin.Communication.Responses.Stocks
{
    public record StockResponse
    {
        public long Id { get; set; }

        public required string Nome { get; set; }

        public required string Codigo { get; set; }

        public required int Quantidade { get; set; }

        public required double PrecoAtual { get; set; }

    }
}
