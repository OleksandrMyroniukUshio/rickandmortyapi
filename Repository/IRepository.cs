using RickAndMortyAPI.Entities;

namespace RickAndMortyAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        //considering ActionResult class
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIDAsync (int entityID);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveAsync();
        Task DeleteAsync(int id);
    }
}
