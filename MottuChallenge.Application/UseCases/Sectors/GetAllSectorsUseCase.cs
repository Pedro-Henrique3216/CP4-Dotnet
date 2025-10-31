using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Sectors;

public class GetAllSectorsUseCase
{
    private readonly ISectorRepository _sectorRepository;

    public GetAllSectorsUseCase(ISectorRepository sectorRepository)
    {
        _sectorRepository = sectorRepository;
    }

    public async Task<List<SectorResponseDto>> ExecuteAsync()
    {
        var sectors = await _sectorRepository.GetAllSectorsAsync();
        var sectorsDto = new List<SectorResponseDto>();

        foreach (var sector in sectors)
        {
            var points = sector.Points.Select(p => new PointResponseDto
            {
                PointOrder = p.PointOrder,
                X = p.X,
                Y = p.Y
            }).ToList();

            var spots = sector.Spots.Select(s => new SpotResponseDto
            {
                SpotId = s.SpotId,
                SectorId = s.SectorId,
                Status = s.Status,
                MotorcycleId = s.MotorcycleId,
                X = s.X,
                Y = s.Y
            }).ToList();

            sectorsDto.Add(new SectorResponseDto
            {
                Id = sector.Id,
                YardId = sector.YardId,
                SectorTypeId = sector.SectorTypeId,
                Points = points,
                Spots = spots
            });
        }

        return sectorsDto;
    }
}