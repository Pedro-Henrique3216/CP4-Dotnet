using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.SectorTypes;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/v{version:apiVersion}/sectors_type")]
    [ApiController]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    public class SectorTypeController : ControllerBase
    {
        private readonly CreateSectorTypeUseCase _createUseCase;
        private readonly GetAllSectorTypesUseCase _getAllUseCase;
        private readonly UpdateSectorTypeUseCase _updateUseCase;
        private readonly DeleteSectorTypeUseCase _deleteUseCase;

        public SectorTypeController(
            CreateSectorTypeUseCase createUseCase,
            GetAllSectorTypesUseCase getAllUseCase,
            UpdateSectorTypeUseCase updateUseCase,
            DeleteSectorTypeUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] SectorTypeDto dto)
        {
            await _createUseCase.ExecuteAsync(dto);
            return Created();
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Get()
        {
            var sectors = await _getAllUseCase.ExecuteAsync();
            return Ok(sectors);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] SectorTypeDto dto)
        {
            var result = await _updateUseCase.ExecuteAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _deleteUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}
