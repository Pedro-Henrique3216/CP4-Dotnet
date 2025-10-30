using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Services;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/v{version:apiVersion}/sectors_type")]
    [ApiController]
    [ApiVersion(1.0)]
    public class SectorTypeController(ISectorTypeService sectorType) : ControllerBase
    {
        private readonly ISectorTypeService _sectorType = sectorType;

        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            await _sectorType.AddSectorType(sectorTypeCreateDto);
            return Created();
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Get()
        {
            var sectors = await _sectorType.GetAllSectorTypesAsync();
            return Ok(sectors);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            var sectorType = await _sectorType.UpdateSectorTypeAsync(sectorTypeCreateDto, id);
            return Ok(sectorType);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _sectorType.DeleteSectorTypeAsync(id);
            return NoContent();
        }
    }
}
