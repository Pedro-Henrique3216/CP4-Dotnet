using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Repositories
{
    public interface ISectorTypeRepository
    {

        public Task<SectorType> SaveSectorTypeAsync(SectorType sectorType);

        public Task<SectorType> FindSectorByName(string name);

        public Task<List<SectorType>> GetAllSectorTypesAsync();

        public Task<SectorType> UpdateSectorTypeAsync(SectorType sectorType);

        public Task<SectorType> FindAsync(Guid id);
    }
}
