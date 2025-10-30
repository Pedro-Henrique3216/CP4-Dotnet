using Microsoft.EntityFrameworkCore;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class SectorRepository(MottuChallengeContext context) : ISectorRepository
    {
        public async Task<Sector> SaveSectorAsync(Sector sector)
        {
            await context.Sectors.AddAsync(sector);
            await context.SaveChangesAsync();
            return sector;
        }

        public async Task<List<Sector>> GetSectorsByYardIdAsync(Guid yardId)
        {
            return await context.Sectors
                                 .Where(s => s.YardId == yardId)
                                 .ToListAsync();
        }

        public async Task<Sector> GetSectorByIdAsync(Guid sectorId)
        {
            return await context.Sectors
                                 .Include(s => s.Spots)
                                 .FirstOrDefaultAsync(s => s.Id == sectorId);
        }

        public async Task<List<Sector>> GetAllSectorsAsync()
        {
            return await context.Sectors
                                 .Include(s => s.Spots)
                                 .ToListAsync();
        }
    }
}
