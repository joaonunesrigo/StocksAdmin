using ProductClientHub.Api.DataBase;
using ProductClientHub.Api.Mappers;
using ProductClientHub.Api.Services.ExternalApiService;
using ProductClientHub.Api.Services.Wallet;
using ProjectClientHub.Communication.Requests.Stock;
using ProjectClientHub.Communication.Responses.Stocks;
using ProjectClientHub.Entities;

namespace ProductClientHub.Api.Services.Stocks
{
    public class StockService
    {
        private readonly DataBaseContext _dbContext;
        private readonly WalletService _walletService;
        private readonly AssetService _assetService;

        public StockService(DataBaseContext dbContext, WalletService walletService, AssetService assetService)
        {
            _dbContext = dbContext;
            _walletService = walletService;
            _assetService = assetService;
        }

        public async Task<StockResponse> CreateStock(StockRequest stockRequest)
        {
            _walletService.VerifyWallet(stockRequest.WalletId);

            var apiResponse = await _assetService.GetAsset(stockRequest.Codigo);

            if (apiResponse?.Results == null || apiResponse.Results.Count == 0)
            {
                throw new Exception("Nenhum dado de ativo encontrado.");
            }

            var stockApi = apiResponse.Results[0];

            var stockEntity = new Stock
            {
                Codigo = stockApi.Symbol ?? "",
                Nome = stockApi.LongName ?? "",
                CurrentPrice = stockApi.RegularMarketPrice,
                Quantidade = stockRequest.Quantidade,
                WalletId = stockRequest.WalletId,
            };

            _dbContext.Stocks.Add(stockEntity);
            await _dbContext.SaveChangesAsync();

            return new StockResponse
            {
                Id = stockEntity.Id,
                Nome = stockEntity.Nome
            };
        }

        public StockResponse UpdateStock(long id, StockRequest stockRequest)
        {
            var stock = _dbContext.Stocks.Find(id);

            if (stock == null)
            {
                throw new Exception("Ativo não encontrado.");
            }

            stock.Quantidade = stockRequest.Quantidade;

            var stockResponse = StockMapper.ToResponse(stock);

            _dbContext.SaveChanges();

            return stockResponse;
        }
        public AllStocksResponse GetAllStocks()
        {
            var stocks = _dbContext.Stocks.ToList();
            var stockResponse = StockMapper.ToResponseList(stocks);

            return new AllStocksResponse()
            {
                Stocks = stockResponse
            };
        }
    }
}
