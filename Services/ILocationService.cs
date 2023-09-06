using RickAndMortyAPI.Entities;

namespace RickAndMortyAPI.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetLocationsAsync();
        Task<LocationDTO> GetLocationAsync(int entityID);
    }
}
