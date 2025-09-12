using Microsoft.EntityFrameworkCore;
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

        public async Task<List<SectorType>> GetAllSectorTypesAsync()
        {
            return await _sectorTypeRepository.GetAllSectorTypesAsync();
        }

        public async Task<SectorType> UpdateSectorTypeAsync(SectorTypeCreateDto dto, Guid id)
        {
            var sectorType = await _sectorTypeRepository.FindAsync(id);
            if (sectorType == null)
                throw new KeyNotFoundException($"SectorType with id {id} not found.");

            sectorType.AlterName(dto.Name);

            await _sectorTypeRepository.UpdateSectorTypeAsync(sectorType);
            return sectorType;
        }
    }
}
