using ProjectClientHub.Communication.Responses.Stocks;
using ProjectClientHub.Entities;

namespace ProductClientHub.Api.Mappers
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
