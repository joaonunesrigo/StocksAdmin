using StocksAdmin.Api.DataBase;
using StocksAdmin.Api.Services.ExternalApiService;

namespace StocksAdmin.Api.Tasks
{
    public class AtualizaPrecoAtivosTask : IHostedService, IDisposable
    {
        private readonly ILogger<AtualizaPrecoAtivosTask> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer? _timer;
        private readonly TimeSpan _intervalo = TimeSpan.FromMinutes(5);

        public AtualizaPrecoAtivosTask(ILogger<AtualizaPrecoAtivosTask> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Execute, null, TimeSpan.Zero, _intervalo);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void Execute(object? state)
        {
            _logger.LogInformation("Atualizando valores dos ativos...");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                var getStockFromApiCommand = scope.ServiceProvider.GetRequiredService<AssetService>();

                var stockLists = dbContext.Stocks.ToList();

                foreach (var stock in stockLists)
                {
                    var stockApiResponse = await getStockFromApiCommand.GetAsset(stock.Codigo);

                    stock.CurrentPrice = stockApiResponse.Results[0].RegularMarketPrice;
                }

                await dbContext.SaveChangesAsync();
            }

            _logger.LogInformation("Valores dos ativos atualizados...");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
