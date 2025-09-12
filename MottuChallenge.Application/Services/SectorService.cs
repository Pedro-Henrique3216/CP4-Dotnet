using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;
using MottuChallenge.Infrastructure.Repositories;

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

        public async Task<SectorResponseDto> GetSectorByIdAsync(Guid sectorId)
        {
            var sector = await _sectorRepository.GetSectorByIdAsync(sectorId);

            var points = createListOfPointResponseDto(sector);

            var spots = createListOfSpotResponseDto(sector);

            return new SectorResponseDto
            {
                Id = sector.Id,
                YardId = sector.YardId,
                SectorTypeId = sector.SectorTypeId,
                Points = points,
                Spots = spots
            };

        }
        public async Task<List<SectorResponseDto>> GetAllSectorsAsync()
        {
            var sectors = await _sectorRepository.GetAllSectorsAsync();

            var sectorsDto = new List<SectorResponseDto>();

            foreach (var sector in sectors)
            {
                var points = createListOfPointResponseDto(sector);

                var spots = createListOfSpotResponseDto(sector);

                var sectorResponseDto = new SectorResponseDto
                {
                    Id = sector.Id,
                    YardId = sector.YardId,
                    SectorTypeId = sector.SectorTypeId,
                    Points = points,
                    Spots = spots
                };

                sectorsDto.Add(sectorResponseDto);
            }

            return sectorsDto;
        }

        private List<PointResponseDto> createListOfPointResponseDto(Sector sector)
        {
            var points = new List<PointResponseDto>();

            foreach (var point in sector.Points)
            {
                points.Add(new PointResponseDto { PointOrder = point.PointOrder, X = point.X, Y = point.Y });
            }

            return points;
        }

        private List<SpotResponseDto> createListOfSpotResponseDto(Sector sector)
        {
            var spots = new List<SpotResponseDto>();

            foreach (var spot in sector.Spots)
            {
                spots.Add(new SpotResponseDto { SpotId = spot.SpotId, SectorId = spot.SectorId, Status = spot.Status, MotorcycleId = spot.MotorcycleId, X = spot.X, Y = spot.Y });
            }

            return spots;
        }
    }
}
