using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.SectorTypes;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Cria um novo tipo de setor", Description = "Cria um tipo de setor com as informações fornecidas no DTO.")]
        [SwaggerResponse(201, "Tipo de setor criado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] SectorTypeDto dto)
        {
            var sectorType = _createUseCase.ExecuteAsync(dto);
            return Created("", sectorType);

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os tipos de setor", Description = "Retorna a lista de todos os tipos de setor cadastrados.")]
        [SwaggerResponse(200, "Lista de tipos de setor retornada com sucesso", typeof(List<SectorTypeDto>))]
        public async Task<IActionResult> Get()
        {
            var sectors = await _getAllUseCase.ExecuteAsync();
            return Ok(sectors);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um tipo de setor", Description = "Atualiza os dados de um tipo de setor existente pelo ID.")]
        [SwaggerResponse(200, "Tipo de setor atualizado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos")]
        [SwaggerResponse(404, "Tipo de setor não encontrado")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] SectorTypeDto dto)
        {
            var result = await _updateUseCase.ExecuteAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove um tipo de setor", Description = "Deleta o tipo de setor correspondente ao ID fornecido.")]
        [SwaggerResponse(204, "Tipo de setor removido com sucesso")]
        [SwaggerResponse(404, "Tipo de setor não encontrado")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _deleteUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}
