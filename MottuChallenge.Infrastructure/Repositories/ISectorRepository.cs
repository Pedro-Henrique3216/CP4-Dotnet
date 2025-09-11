using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Repositories
{
    public interface ISectorRepository
    {

        public Task<Sector> SaveSectorAsync(Sector sector);

        public Task<List<Sector>> GetSectorsByYardIdAsync(Guid yardId);
    }
}
