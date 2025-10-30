using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.Services
{
    public class SectorTypeService(ISectorTypeRepository sectorTypeRepository) : ISectorTypeService
    {
        public async Task<SectorType> AddSectorType(SectorTypeDto sectorTypeCreateDto)
        {
            var verifySector = await sectorTypeRepository.FindSectorByName(sectorTypeCreateDto.Name.ToLower());
            if (verifySector != null)
            {
                throw new Exception("Ja existe sectorType com esse nome");
            }

            var sectorType = new SectorType(sectorTypeCreateDto.Name.ToLower());

            await sectorTypeRepository.SaveSectorTypeAsync(sectorType);
            return sectorType;
        }

        public async Task<List<SectorTypeResponseDto>> GetAllSectorTypesAsync()
        {
            var sectorTypes =  await sectorTypeRepository.GetAllSectorTypesAsync();
            return sectorTypes.Select(st => new SectorTypeResponseDto
            {
                Id = st.Id,
                Name = st.Name
            }).ToList();
        }

        public async Task<SectorTypeResponseDto> UpdateSectorTypeAsync(SectorTypeDto dto, Guid id)
        {
            var sectorType = await sectorTypeRepository.FindAsync(id);
            if (sectorType == null)
                throw new KeyNotFoundException($"SectorType with id {id} not found.");

            sectorType.AlterName(dto.Name);

            await sectorTypeRepository.UpdateSectorTypeAsync(sectorType);

            return new SectorTypeResponseDto
            {
                Id = sectorType.Id,
                Name = sectorType.Name
            };
        }

        public async Task DeleteSectorTypeAsync(Guid id)
        {
            var sectorType = await sectorTypeRepository.FindAsync(id);
            await sectorTypeRepository.DeleteSectorTypeAsync(sectorType);

        }
    }
}
