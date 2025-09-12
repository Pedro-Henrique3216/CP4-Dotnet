using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface ISectorService
    {
        Task<Sector> SaveSectorAsync(SectorCreateDto sectorCreateDto);
        Task<SectorResponseDto> GetSectorByIdAsync(Guid sectorId);
        Task<List<SectorResponseDto>> GetAllSectorsAsync();
    }
}
