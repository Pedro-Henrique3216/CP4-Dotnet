using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Services;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/v{version:apiVersion}/yards")]
    [ApiController]
    [ApiVersion(2.0)]
    public class YardController(IYardService yardService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] CreateYardDto createYardDto)
        {
            await yardService.SaveYardAsync(createYardDto);
            return Created();
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> GetAllYardsAsync()
        {
            var yards = await yardService.GetAllYardsAsync();
            return Ok(yards);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var yard = await yardService.GetYardResponseByIdAsync(id);
            return Ok(yard);
        }
       
    }
}
