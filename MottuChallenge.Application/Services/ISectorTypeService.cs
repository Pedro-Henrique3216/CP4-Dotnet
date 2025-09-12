using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface ISectorTypeService
    {

        public Task<SectorType> AddSectorType(SectorTypeCreateDto sectorTypeCreateDto);

        public Task<List<SectorType>> GetAllSectorTypesAsync();

        public Task<SectorType> UpdateSectorTypeAsync(SectorTypeCreateDto dto, Guid id);

        public Task DeleteSectorTypeAsync(Guid id);
    }
}
