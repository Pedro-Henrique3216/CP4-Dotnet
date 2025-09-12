using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface ISectorTypeService
    {

        Task<SectorType> AddSectorType(SectorTypeDto sectorTypeCreateDto);

        Task<List<SectorTypeResponseDto>> GetAllSectorTypesAsync();

        Task<SectorTypeResponseDto> UpdateSectorTypeAsync(SectorTypeDto dto, Guid id);

        Task DeleteSectorTypeAsync(Guid id);
    }
}
