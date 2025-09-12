using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Services;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/sectors")]
    [ApiController]
    public class SectorController(ISectorService sectorService) : ControllerBase
    {
        private readonly ISectorService _sectorService = sectorService;

        [HttpPost]
        public async Task Post([FromBody] SectorCreateDto sectorCreateDto)
        {
            await _sectorService.SaveSectorAsync(sectorCreateDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllYardsAsync()
        {
            var yards = await _sectorService.GetAllSectorsAsync();
            return Ok(yards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var yard = await _sectorService.GetSectorByIdAsync(id);
            return Ok(yard);
        }

    }
}
