using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.Yards;
using Swashbuckle.AspNetCore.Annotations;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/v{version:apiVersion}/yards")]
    [ApiController]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    public class YardController : ControllerBase
    {
        private readonly CreateYardUseCase _createYardUseCase;
        private readonly GetAllYardsUseCase _getAllYardsUseCase;
        private readonly GetYardByIdUseCase _getYardByIdUseCase;

        public YardController(
            CreateYardUseCase createYardUseCase,
            GetAllYardsUseCase getAllYardsUseCase,
            GetYardByIdUseCase getYardByIdUseCase)
        {
            _createYardUseCase = createYardUseCase;
            _getAllYardsUseCase = getAllYardsUseCase;
            _getYardByIdUseCase = getYardByIdUseCase;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo pátio", Description = "Cria um pátio com as informações fornecidas no DTO.")]
        [SwaggerResponse(201, "Pátio criado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] CreateYardDto createYardDto)
        {
            var yard = await _createYardUseCase.ExecuteAsync(createYardDto);
            return CreatedAtAction(nameof(GetById), new { id = yard.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() }, yard);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os pátios", Description = "Retorna a lista de todos os pátios cadastrados.")]
        [SwaggerResponse(200, "Lista de pátios retornada com sucesso", typeof(List<CreateYardDto>))]
        public async Task<IActionResult> GetAllYardsAsync()
        {
            var yards = await _getAllYardsUseCase.ExecuteAsync();
            return Ok(yards);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca pátio por ID", Description = "Retorna os dados do pátio correspondente ao ID fornecido.")]
        [SwaggerResponse(200, "Pátio encontrado", typeof(CreateYardDto))]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var yard = await _getYardByIdUseCase.ExecuteAsync(id);
            if (yard == null)
                return NotFound();

            return Ok(yard);
        }
    }
}
