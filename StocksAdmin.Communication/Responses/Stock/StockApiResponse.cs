using System.Text.Json.Serialization;

namespace ProjectClientHub.Communication.Responses.Stocks
{
    public class StockApiResponse
    {
        [JsonPropertyName("results")]
        public List<StockApiResult> Results { get; set; } = new();
    }

    public class StockApiResult
    {
        [JsonPropertyName("longName")]
        public string? LongName { get; set; }

        [JsonPropertyName("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
    }
}
