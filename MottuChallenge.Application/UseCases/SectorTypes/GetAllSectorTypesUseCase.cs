using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.SectorTypes;

public class GetAllSectorTypesUseCase
{
    private readonly ISectorTypeRepository _sectorTypeRepository;

    public GetAllSectorTypesUseCase(ISectorTypeRepository sectorTypeRepository)
    {
        _sectorTypeRepository = sectorTypeRepository;
    }

    public async Task<List<SectorTypeResponseDto>> ExecuteAsync()
    {
        var sectorTypes = await _sectorTypeRepository.GetAllSectorTypesAsync();

        return sectorTypes.Select(st => new SectorTypeResponseDto
        {
            Id = st.Id,
            Name = st.Name
        }).ToList();
    }
}