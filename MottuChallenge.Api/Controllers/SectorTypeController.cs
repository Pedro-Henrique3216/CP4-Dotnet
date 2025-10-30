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
    public class SectorTypeController(ISectorTypeService type) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            await type.AddSectorType(sectorTypeCreateDto);
            return Created();
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Get()
        {
            var sectors = await type.GetAllSectorTypesAsync();
            return Ok(sectors);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            var sectorType = await type.UpdateSectorTypeAsync(sectorTypeCreateDto, id);
            return Ok(sectorType);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await type.DeleteSectorTypeAsync(id);
            return NoContent();
        }
    }
}
