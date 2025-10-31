using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.UseCases.Sectors;
using Swashbuckle.AspNetCore.Annotations;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/v{version:apiVersion}/sectors")]
    [ApiController]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    public class SectorController : ControllerBase
    {
        private readonly CreateSectorUseCase _createSectorUseCase;
        private readonly GetAllSectorsUseCase _getAllSectorsUseCase;
        private readonly GetSectorByIdUseCase _getSectorByIdUseCase;

        public SectorController(
            CreateSectorUseCase createSectorUseCase,
            GetAllSectorsUseCase getAllSectorsUseCase,
            GetSectorByIdUseCase getSectorByIdUseCase)
        {
            _createSectorUseCase = createSectorUseCase;
            _getAllSectorsUseCase = getAllSectorsUseCase;
            _getSectorByIdUseCase = getSectorByIdUseCase;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo setor", Description = "Cria um setor com as informações fornecidas no DTO.")]
        [SwaggerResponse(201, "Setor criado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] SectorCreateDto dto)
        {
            var createdSector = await _createSectorUseCase.ExecuteAsync(dto);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdSector.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() },
                createdSector
            );
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os setores", Description = "Retorna a lista de todos os setores cadastrados.")]
        [SwaggerResponse(200, "Lista de setores retornada com sucesso", typeof(List<SectorResponseDto>))]
        public async Task<IActionResult> GetAllSectorsAsync()
        {
            var sectors = await _getAllSectorsUseCase.ExecuteAsync();
            return Ok(sectors);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca setor por ID", Description = "Retorna os dados do setor correspondente ao ID fornecido.")]
        [SwaggerResponse(200, "Setor encontrado", typeof(SectorResponseDto))]
        [SwaggerResponse(404, "Setor não encontrado")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var sector = await _getSectorByIdUseCase.ExecuteAsync(id);
            if (sector == null)
                return NotFound();

            return Ok(sector);
        }
    }
}
