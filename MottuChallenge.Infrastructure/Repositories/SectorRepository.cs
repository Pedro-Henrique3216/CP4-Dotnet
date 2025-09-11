using Microsoft.EntityFrameworkCore;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class SectorRepository(MottuChallengeContext context) : ISectorRepository
    {

        private readonly MottuChallengeContext _context = context;

        public async Task<Sector> SaveSectorAsync(Sector sector)
        {
            await _context.Sectors.AddAsync(sector);
            await _context.SaveChangesAsync();
            return sector;
        }

        public async Task<List<Sector>> GetSectorsByYardIdAsync(Guid yardId)
        {
            return await _context.Sectors
                                 .Where(s => s.YardId == yardId)
                                 .ToListAsync();
        }
    }
}
