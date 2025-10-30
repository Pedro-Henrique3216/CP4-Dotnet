using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Services;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/v{version:apiVersion}/sectors")]
    [ApiController]
    [ApiVersion(1.0)]
    public class SectorController(ISectorService sectorService) : ControllerBase
    {
        private readonly ISectorService _sectorService = sectorService;

        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] SectorCreateDto sectorCreateDto)
        {
            await _sectorService.SaveSectorAsync(sectorCreateDto);
            return Created();
        }

       
        [HttpGet]
        [ProducesResponseType(typeof(List<SectorResponseDto>), 200)]
        public async Task<IActionResult> GetAllSectorsAsync()
        {
            var sectors = await _sectorService.GetAllSectorsAsync();
            return Ok(sectors);
        }

      
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SectorResponseDto), 200)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var sector = await _sectorService.GetSectorByIdAsync(id);
            return Ok(sector);
        }
    }
}
