using Microsoft.EntityFrameworkCore;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class SectorTypeRepository(MottuChallengeContext context) : ISectorTypeRepository
    {
        public async Task<SectorType> FindSectorByName(string name)
        {
            return await context.SectorTypes
                        .FirstOrDefaultAsync(s => s.Name == name);

        }

        public async Task<SectorType> SaveSectorTypeAsync(SectorType sectorType)
        {
            await context.SectorTypes.AddAsync(sectorType);
            await context.SaveChangesAsync();
            return sectorType;
        }

        public async Task<List<SectorType>> GetAllSectorTypesAsync()
        {
            return await context.SectorTypes.ToListAsync();
        }
        public async Task<SectorType> UpdateSectorTypeAsync(SectorType sectorType)
        {
            context.SectorTypes.Update(sectorType);
            await context.SaveChangesAsync();
            return sectorType;
        }

        public async Task<SectorType> FindAsync(Guid id)
        {
            return await context.SectorTypes.FindAsync(id);
        }

        public async Task DeleteSectorTypeAsync(SectorType sectorType)
        {
            context.SectorTypes.Remove(sectorType);
            await context.SaveChangesAsync();
        }



    }
}
