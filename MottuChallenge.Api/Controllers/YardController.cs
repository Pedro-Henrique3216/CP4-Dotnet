using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Services;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/yards")]
    [ApiController]
    public class YardController(IYardService yardService) : ControllerBase
    {
        private readonly IYardService _yardService = yardService;

        [HttpPost]
        public async Task Post([FromBody] CreateYardDto createYardDto)
        {
            await _yardService.SaveYardAsync(createYardDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllYardsAsync()
        {
            var yards = await _yardService.GetAllYardsAsync();
            return Ok(yards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var yard = await _yardService.GetYardResponseByIdAsync(id);
            return Ok(yard);
        }
       
    }
}
