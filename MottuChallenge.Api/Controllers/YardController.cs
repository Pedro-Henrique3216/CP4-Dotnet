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

    }
}
