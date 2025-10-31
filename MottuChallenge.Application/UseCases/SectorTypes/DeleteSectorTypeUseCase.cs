using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.SectorTypes;

public class DeleteSectorTypeUseCase
{
    private readonly ISectorTypeRepository _sectorTypeRepository;

    public DeleteSectorTypeUseCase(ISectorTypeRepository sectorTypeRepository)
    {
        _sectorTypeRepository = sectorTypeRepository;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var sectorType = await _sectorTypeRepository.FindAsync(id);
        if (sectorType == null)
            throw new KeyNotFoundException($"SectorType com id {id} não foi encontrado.");

        await _sectorTypeRepository.DeleteSectorTypeAsync(sectorType);
    }
}