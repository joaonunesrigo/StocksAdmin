using StocksAdmin.Api.DataBase;
using StocksAdmin.Api.Mappers;
using StocksAdmin.Communication.Requests.Wallet;
using StocksAdmin.Communication.Responses.Wallets;

namespace StocksAdmin.Api.Services.Wallet
{
    public class WalletService
    {
        private readonly DataBaseContext _dbContext;

        public WalletService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WalletResponse CreateWallet(WalletRequest walletRequest)
        {
            var walletEntity = new Entities.Wallet
            {
                Name = walletRequest.Nome,
            };

            _dbContext.Wallets.Add(walletEntity);

            _dbContext.SaveChanges();

            return new WalletResponse
            {
                Id = walletEntity.Id,
                Name = walletEntity.Name
            };
        }

        public AllWalletsResponse GetAllUserWallets()
        {
            var wallets = _dbContext.Wallets.ToList();
            var walletResponses = WalletMapper.ToResponseList(wallets);

            return new AllWalletsResponse()
            {
                Wallets = walletResponses
            };
        }

        public WalletResponse UpdateWallet(long id, WalletRequest walletRequest)
        {
            var wallet = _dbContext.Wallets.Find(id);

            if (wallet == null)
            {
                throw new Exception("Carteira não encontrada.");
            }

            wallet.Name = walletRequest.Nome;

            var walletResponse = WalletMapper.ToResponse(wallet);

            _dbContext.SaveChanges();

            return walletResponse;
        }

        public void VerifyWallet(long walletId)
        {
            bool existsWallet = _dbContext.Wallets.Any(w => w.Id == walletId);

            if (!existsWallet)
            {
                throw new ArgumentException($"A carteira de Id: {walletId} não existe ou é inválida.");
            }
        }
    }
}
