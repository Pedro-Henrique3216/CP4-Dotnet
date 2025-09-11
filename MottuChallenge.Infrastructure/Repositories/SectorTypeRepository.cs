using Microsoft.EntityFrameworkCore;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class SectorTypeRepository(MottuChallengeContext context) : ISectorTypeRepository
    {

        private readonly MottuChallengeContext _context = context;

        public async Task<SectorType> FindSectorByName(string name)
        {
            return await _context.SectorTypes
                        .FirstOrDefaultAsync(s => s.Name == name);

        }

        public async Task<SectorType> SaveSectorTypeAsync(SectorType sectorType)
        {
            await _context.SectorTypes.AddAsync(sectorType);
            await _context.SaveChangesAsync();
            return sectorType;
        }

        public async Task<List<SectorType>> GetAllSectorTypesAsync()
        {
            return await _context.SectorTypes.ToListAsync();
        }


    }
}
