using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;
using MottuChallenge.Infrastructure.Repositories;
using System.ComponentModel;

namespace MottuChallenge.Application.Services
{
    public class SectorService(ISectorRepository sectorRepository, IYardService yardService, ISpotService spotService) : ISectorService
    {
        private readonly ISectorRepository _sectorRepository = sectorRepository;
        private readonly IYardService _yardService = yardService;
        private readonly ISpotService _spotService = spotService;

        public async Task<Sector> SaveSectorAsync(SectorCreateDto sectorCreateDto)
        {

            var sector = new Sector(sectorCreateDto.SectorTypeId, sectorCreateDto.YardId);

            var yard = await _yardService.GetYardByIdAsync(sector.YardId);

            ValidateYardExists(yard);

            ValidateSectorInsideYard(sector, yard);

            var existingSectors = await _sectorRepository.GetSectorsByYardIdAsync(yard.Id);

            ValidateSectorOverlap(sector, existingSectors);

            foreach (var point in sectorCreateDto.Points)
            {
                sector.AddPoint(new PolygonPoint(point.PointOrder, point.X, point.Y));
            }

            var spots = _spotService.GenerateSpot(sector, 2, 2);

            foreach (var spot in spots)
            {
                sector.AddSpot(spot);
            }

            return await _sectorRepository.SaveSectorAsync(sector);
        }


        private void ValidateSectorInsideYard(Sector sector, Yard yard)
        {
            bool isInside = sector.Points.All(p => GeometryHelper.IsPointInsidePolygon(p.X, p.Y, yard.Points.ToList()));
            if (!isInside)
            {
                throw new InvalidOperationException("Sector is not fully inside the Yard.");
            }
        }

        private void ValidateSectorOverlap(Sector sector, List<Sector> existingSectors)
        {
            foreach (var existingSector in existingSectors)
            {
                bool overlap = sector.Points.Any(p =>
                    GeometryHelper.IsPointInsidePolygon(p.X, p.Y, existingSector.Points.ToList()));
                if (overlap)
                {
                    throw new InvalidOperationException("Sector overlaps with another existing sector.");
                }
            }
        }

        private void ValidateYardExists(Yard yard)
        {
            if (yard == null)
            {
                throw new InvalidOperationException("Yard does not exist.");
            }
        }

        public async Task<Sector> GetSectorByIdAsync(Guid sectorId)
        {
            return await _sectorRepository.GetSectorByIdAsync(sectorId);
        }
        public async Task<List<Sector>> GetAllSectorsAsync()
        {
            return await _sectorRepository.GetAllSectorsAsync();
        }
    }
}
