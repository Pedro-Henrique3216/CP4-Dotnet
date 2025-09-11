using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface ISectorService
    {
        public Task<Sector> SaveSectorAsync(SectorCreateDto sectorCreateDto);
    }
}
