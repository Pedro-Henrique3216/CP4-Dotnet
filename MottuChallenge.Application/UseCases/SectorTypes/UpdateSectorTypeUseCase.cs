using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.SectorTypes;

public class UpdateSectorTypeUseCase
{
    private readonly ISectorTypeRepository _sectorTypeRepository;

    public UpdateSectorTypeUseCase(ISectorTypeRepository sectorTypeRepository)
    {
        _sectorTypeRepository = sectorTypeRepository;
    }

    public async Task<SectorTypeResponseDto> ExecuteAsync(Guid id, SectorTypeDto dto)
    {
        var sectorType = await _sectorTypeRepository.FindAsync(id);
        if (sectorType == null)
            throw new KeyNotFoundException($"SectorType com id {id} não foi encontrado.");

        sectorType.AlterName(dto.Name);

        await _sectorTypeRepository.UpdateSectorTypeAsync(sectorType);

        return new SectorTypeResponseDto
        {
            Id = sectorType.Id,
            Name = sectorType.Name
        };
    }
}