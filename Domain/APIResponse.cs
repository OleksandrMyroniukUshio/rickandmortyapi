using RickAndMortyAPI.Entities;

namespace RickAndMortyAPI.Domain
{
    public class APIResponse
    {
        public Info? Info { get; set; }
        public IEnumerable<LocationAPIResponse>? Results { get; set; }
    }
}
