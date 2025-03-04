using StocksAdmin.Api.Entities;
using StocksAdmin.Communication.Responses.Stocks;

namespace StocksAdmin.Api.Mappers
{
    public class StockMapper
    {
        public static StockResponse ToResponse(Stock stock)
        {
            return new StockResponse
            {
                Id = stock.Id,
                Nome = stock.Nome,
            };
        }

        public static List<StockResponse> ToResponseList(List<Stock> stocks) => stocks.Select(s => ToResponse(s)).ToList();
    }
}
