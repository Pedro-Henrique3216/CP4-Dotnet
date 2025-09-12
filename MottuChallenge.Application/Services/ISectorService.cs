using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface ISectorService
    {
        public Task<Sector> SaveSectorAsync(SectorCreateDto sectorCreateDto);
        public Task<SectorResponseDto> GetSectorByIdAsync(Guid sectorId);
        public Task<List<SectorResponseDto>> GetAllSectorsAsync();
    }
}
