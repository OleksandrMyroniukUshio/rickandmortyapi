using RickAndMortyAPI.Middleware;

namespace RickAndMortyAPI.Repository
{
    public class UnitOfWork
    {
        private readonly RickAndMortyContext _dbcontext;
        private LocationRepository _repository;
        public UnitOfWork(RickAndMortyContext dbcontext) {
            _dbcontext = dbcontext;
        }
        public LocationRepository Repository { 
            get { _repository ??= new LocationRepository(_dbcontext);  return _repository; }
        }
    }
}
