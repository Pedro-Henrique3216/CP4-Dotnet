using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;
using MottuChallenge.Infrastructure.Repositories;
using MottuChallenge.Application.UseCases.Spots;
using MottuChallenge.Application.UseCases.Yards;

namespace MottuChallenge.Application.UseCases.Sectors
{
    public class CreateSectorUseCase
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly GetYardByIdUseCase _getYardByIdUseCase;
        private readonly SpotUseCase _spotUseCase;
        private readonly IYardRepository _yardRepository;

        public CreateSectorUseCase(
            ISectorRepository sectorRepository,
            IYardRepository yardRepository,
            SpotUseCase spotUseCase)
        {
            _sectorRepository = sectorRepository;
            _spotUseCase = spotUseCase;
            _yardRepository = yardRepository;
        }

        public async Task<Sector> ExecuteAsync(SectorCreateDto dto)
        {
            var sector = new Sector(dto.SectorTypeId, dto.YardId);
            
            var yard = await _yardRepository.GetYardByIdAsync(sector.YardId);
            ValidateYardExists(yard);

            // Monta os pontos do setor
            foreach (var point in dto.Points)
            {
                sector.AddPoint(new PolygonPoint(point.PointOrder, point.X, point.Y));
            }
            
            ValidateSectorInsideYard(sector, yard);

            // Valida sobreposição com outros setores
            var existingSectors = await _sectorRepository.GetSectorsByYardIdAsync(yard.Id);
            ValidateSectorOverlap(sector, existingSectors);

            // Gera os spots automaticamente
            var spots = _spotUseCase.GenerateSpot(sector, 2, 2);
            foreach (var spot in spots)
            {
                sector.AddSpot(spot);
            }

            // Salva no repositório
            return await _sectorRepository.SaveSectorAsync(sector);
        }

        // 🔒 Validações

        private void ValidateYardExists(Yard? yard)
        {
            if (yard is null)
                throw new InvalidOperationException("Yard does not exist.");
        }

        private void ValidateSectorInsideYard(Sector sector, Yard yard)
        {
            bool isInside = sector.Points.All(p =>
                GeometryHelper.IsPointInsidePolygon(p.X, p.Y, yard.Points.ToList()));

            if (!isInside)
                throw new InvalidOperationException("Sector is not fully inside the Yard.");
        }

        private void ValidateSectorOverlap(Sector sector, List<Sector> existingSectors)
        {
            foreach (var existingSector in existingSectors)
            {
                bool overlap = sector.Points.Any(p =>
                    GeometryHelper.IsPointInsidePolygon(p.X, p.Y, existingSector.Points.ToList()));

                if (overlap)
                    throw new InvalidOperationException("Sector overlaps with another existing sector.");
            }
        }
    }
}
