using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Services;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/sectors_type")]
    [ApiController]
    public class SectorTypeController(ISectorTypeService sectorType) : ControllerBase
    {
        private readonly ISectorTypeService _sectorType = sectorType;

        [HttpPost]
        public async Task Post([FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            await _sectorType.AddSectorType(sectorTypeCreateDto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sectors = await _sectorType.GetAllSectorTypesAsync();
            return Ok(sectors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            var sectorType = await _sectorType.UpdateSectorTypeAsync(sectorTypeCreateDto, id);
            return Ok(sectorType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _sectorType.DeleteSectorTypeAsync(id);
            return NoContent();
        }
    }
}
