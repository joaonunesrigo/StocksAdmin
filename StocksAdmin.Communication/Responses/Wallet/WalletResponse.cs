namespace StocksAdmin.Communication.Responses.Wallets
{
    public record WalletResponse
    {
        public long Id { get; set; }

        public required string Name { get; set; }
    }
}
