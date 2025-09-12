using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface ISectorTypeService
    {

        public Task<SectorType> AddSectorType(SectorTypeDto sectorTypeCreateDto);

        public Task<List<SectorTypeResponseDto>> GetAllSectorTypesAsync();

        public Task<SectorTypeResponseDto> UpdateSectorTypeAsync(SectorTypeDto dto, Guid id);

        public Task DeleteSectorTypeAsync(Guid id);
    }
}
