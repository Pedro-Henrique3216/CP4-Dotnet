using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.Services
{
    public class SectorTypeService(ISectorTypeRepository sectorTypeRepository) : ISectorTypeService
    {
        private readonly ISectorTypeRepository _sectorTypeRepository = sectorTypeRepository;

        public async Task<SectorType> AddSectorType(SectorTypeCreateDto sectorTypeCreateDto)
        {
            var verifySector = await _sectorTypeRepository.FindSectorByName(sectorTypeCreateDto.Name.ToLower());
            if (verifySector != null)
            {
                throw new Exception("sada");
            }

            var sectorType = new SectorType(sectorTypeCreateDto.Name.ToLower());

            await _sectorTypeRepository.SaveSectorTypeAsync(sectorType);
            return sectorType;
        }
    }
}
