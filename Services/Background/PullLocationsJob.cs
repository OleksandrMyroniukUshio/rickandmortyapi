using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RickAndMortyAPI.Domain;
using RickAndMortyAPI.Entities;
using RickAndMortyAPI.Repository;
using System;
using System.Net.Http;
using System.Text.Json;

namespace RickAndMortyAPI.Services.Background
{
    public class PullLocationsJob : IPullLocationsJob
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly LocationRepository _repository;
        private readonly ILogger<PullLocationsJob> _logger;
        private readonly IMemoryCache _memoryCache;
        public PullLocationsJob(
            IHttpClientFactory clientFactory, UnitOfWork unit,
            ILogger<PullLocationsJob> logger, IMemoryCache memoryCache)
        {
            _clientFactory = clientFactory;
            _repository = unit.Repository;
            _logger = logger;
            _memoryCache = memoryCache;
        }
        public async Task RunAsync()
        {
            var client = _clientFactory.CreateClient();
            if (!_memoryCache.TryGetValue("nextPageUrl", out string? nextPageUrl) || string.IsNullOrEmpty(nextPageUrl))
            {
                nextPageUrl = "https://rickandmortyapi.com/api/location";
            }
            var response = await client.GetAsync(nextPageUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var locationsResponse = JsonConvert.DeserializeObject<APIResponse>(content);
            foreach (var location in locationsResponse.Results)
            {
                var dbLocation = await _repository.GetByIDAsync(location.Id);
                if (dbLocation == null)
                {
                    await _repository.InsertAsync(new Location
                    {
                        Id = location.Id,
                        Name = location.Name,
                        Type = location.Type,
                        Dimension = location.Dimension
                    });
                }
                else
                {
                    dbLocation.Name = location.Name;
                    dbLocation.Type = location.Type;
                    dbLocation.Dimension = location.Dimension;
                }
            }
            nextPageUrl = locationsResponse.Info.Next;
            var options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(61)
            };
            _memoryCache.Set("nextPageUrl", nextPageUrl, options);
            await _repository.SaveAsync();
            _logger.LogInformation("Data loaded to database.");
        }
    }
}