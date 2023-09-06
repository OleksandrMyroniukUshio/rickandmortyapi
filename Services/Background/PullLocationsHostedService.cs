using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RickAndMortyAPI.Middleware;
using RickAndMortyAPI.Repository;

namespace RickAndMortyAPI.Services.Background
{
    public class PullLocationsHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PullLocationsHostedService> _logger;
        public PullLocationsHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<PullLocationsHostedService> logger) {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var _job = scope.ServiceProvider.GetRequiredService<IPullLocationsJob>(); 
                    await _job.RunAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation("Pulling job ended.");
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
