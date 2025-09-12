using Microsoft.EntityFrameworkCore;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.Services
{
    public class SectorTypeService(ISectorTypeRepository sectorTypeRepository) : ISectorTypeService
    {
        private readonly ISectorTypeRepository _sectorTypeRepository = sectorTypeRepository;

        public async Task<SectorType> AddSectorType(SectorTypeDto sectorTypeCreateDto)
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

        public async Task<List<SectorTypeResponseDto>> GetAllSectorTypesAsync()
        {
            var sectorTypes =  await _sectorTypeRepository.GetAllSectorTypesAsync();
            return sectorTypes.Select(st => new SectorTypeResponseDto
            {
                Id = st.Id,
                Name = st.Name
            }).ToList();
        }

        public async Task<SectorTypeResponseDto> UpdateSectorTypeAsync(SectorTypeDto dto, Guid id)
        {
            var sectorType = await _sectorTypeRepository.FindAsync(id);
            if (sectorType == null)
                throw new KeyNotFoundException($"SectorType with id {id} not found.");

            sectorType.AlterName(dto.Name);

            await _sectorTypeRepository.UpdateSectorTypeAsync(sectorType);

            return new SectorTypeResponseDto
            {
                Id = sectorType.Id,
                Name = sectorType.Name
            };
        }

        public async Task DeleteSectorTypeAsync(Guid id)
        {
            var sectorType = await _sectorTypeRepository.FindAsync(id);
            await _sectorTypeRepository.DeleteSectorTypeAsync(sectorType);

        }
    }
}
