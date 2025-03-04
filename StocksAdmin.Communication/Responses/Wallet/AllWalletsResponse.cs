namespace StocksAdmin.Communication.Responses.Wallets
{
    public record AllWalletsResponse
    {
        public List<WalletResponse> Wallets { get; set; } = [];
    }
}
