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

    }
}
