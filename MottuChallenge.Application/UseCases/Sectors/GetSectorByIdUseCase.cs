using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Sectors;

public class GetSectorByIdUseCase
{
    private readonly ISectorRepository _sectorRepository;

    public GetSectorByIdUseCase(ISectorRepository sectorRepository)
    {
        _sectorRepository = sectorRepository;
    }

    public async Task<SectorResponseDto?> ExecuteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("ID inválido.");

        var sector = await _sectorRepository.GetSectorByIdAsync(id);
        if (sector == null)
            return null;

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

        return new SectorResponseDto
        {
            Id = sector.Id,
            YardId = sector.YardId,
            SectorTypeId = sector.SectorTypeId,
            Points = points,
            Spots = spots
        };
    }
}