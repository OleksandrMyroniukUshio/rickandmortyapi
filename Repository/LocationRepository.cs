using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RickAndMortyAPI.Entities;
using RickAndMortyAPI.Middleware;

namespace RickAndMortyAPI.Repository
{
    public class LocationRepository : IRepository<Location>
    {
        private readonly RickAndMortyContext _dbcontext;
        public LocationRepository(RickAndMortyContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var locations = _dbcontext.Locations;
            return locations;
        }
        public async Task<Location>? GetByIDAsync(int entityID)
        {
            var locations = await _dbcontext.Locations.FindAsync(entityID);
            return locations;
        }
        public async Task InsertAsync(Location entity)
        {
            await _dbcontext.Locations.AddAsync(entity);
        }
        public async Task SaveAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Location entity)
        {
            await Task.Run(() => {
                _dbcontext.Entry(entity).State = EntityState.Modified;
            });
        }
        public async Task DeleteAsync(int id)
        {
            var location = await _dbcontext.Locations.FindAsync(id);
            if (location != null)
            {
                _dbcontext.Remove(location);
            }
        }
    }
}
