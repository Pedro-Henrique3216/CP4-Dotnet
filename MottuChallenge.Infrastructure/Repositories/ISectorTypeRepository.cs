using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Repositories
{
    public interface ISectorTypeRepository
    {

        public Task<SectorType> SaveSectorTypeAsync(SectorType sectorType);

        public Task<SectorType> FindSectorByName(string name);

        public Task<List<SectorType>> GetAllSectorTypesAsync();

    }
}
