using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.SectorTypes;

public class CreateSectorTypeUseCase
{
    private readonly ISectorTypeRepository _sectorTypeRepository;

    public CreateSectorTypeUseCase(ISectorTypeRepository sectorTypeRepository)
    {
        _sectorTypeRepository = sectorTypeRepository;
    }

    public async Task<SectorType> ExecuteAsync(SectorTypeDto dto)
    {
        var existingSector = await _sectorTypeRepository.FindSectorByName(dto.Name.ToLower());
        if (existingSector != null)
            throw new InvalidOperationException("Já existe um SectorType com esse nome.");

        var sectorType = new SectorType(dto.Name.ToLower());
        await _sectorTypeRepository.SaveSectorTypeAsync(sectorType);

        return sectorType;
    }
}