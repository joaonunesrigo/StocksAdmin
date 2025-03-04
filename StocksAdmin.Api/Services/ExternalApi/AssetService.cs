using StocksAdmin.Communication.Responses.Stocks;
using System.Text.Json;

namespace StocksAdmin.Api.Services.ExternalApiService
{
    public class AssetService
    {
        private readonly HttpClient _httpClient;

        public AssetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<StockApiResponse> GetAsset(string codigo)
        {
            try
            {
                string externalApiUrl = $"{ExternalApiConfig.BaseUrl}{codigo}?token={ExternalApiConfig.UrlToken}";
                var response = await _httpClient.GetAsync(externalApiUrl);

                if (!response.IsSuccessStatusCode) throw new Exception($"Não foi possível achar o ativo de código: {codigo}");

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var stockData = JsonSerializer.Deserialize<StockApiResponse>(jsonResponse);

                if (stockData == null) return new StockApiResponse();

                return stockData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
