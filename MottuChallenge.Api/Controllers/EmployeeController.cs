using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.Employees;
using MottuChallenge.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace MottuChallenge.Api.Controllers;

[Route("api/v{version:apiVersion}/employees")]
[ApiController]
[ApiVersion(2.0)]
public class EmployeeController : ControllerBase
{
    private readonly CreateEmployeeUseCase _create;
    private readonly GetAllEmployeesUseCase _getAll;
    private readonly GetEmployeeByEmailUseCase _getByEmail;
    private readonly UpdateEmployeeUseCase _update;
    private readonly DeleteEmployeeUseCase _delete;

    public EmployeeController(
        CreateEmployeeUseCase create,
        GetAllEmployeesUseCase getAll,
        GetEmployeeByEmailUseCase getByEmail,
        UpdateEmployeeUseCase update,
        DeleteEmployeeUseCase delete)
    {
        _create = create;
        _getAll = getAll;
        _getByEmail = getByEmail;
        _update = update;
        _delete = delete;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria um novo funcionário", Description = "Cria um funcionário com nome, email, yardId e senha.")]
    [SwaggerResponse(201, "Funcionário criado com sucesso")]
    [SwaggerResponse(400, "Dados inválidos")]
    public async Task<IActionResult> Post([FromBody] EmployeeDto employeeDto)
    {
        var employee = new Employee(employeeDto.Name, employeeDto.Email, employeeDto.YardId, employeeDto.Password);
        await _create.ExecuteAsync(employee);
        return Created("", null);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todos os funcionários", Description = "Retorna todos os funcionários cadastrados.")]
    [SwaggerResponse(200, "Lista de funcionários retornada com sucesso")]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _getAll.ExecuteAsync();
        return Ok(employees);
    }

    [HttpGet("{email}")]
    [SwaggerOperation(Summary = "Busca funcionário por email", Description = "Retorna os dados do funcionário correspondente ao email fornecido.")]
    [SwaggerResponse(200, "Funcionário encontrado", typeof(Employee))]
    [SwaggerResponse(404, "Funcionário não encontrado")]
    public async Task<IActionResult> GetByEmail([FromRoute] string email)
    {
        var employee = await _getByEmail.ExecuteAsync(email);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Atualiza um funcionário", Description = "Atualiza os dados de um funcionário existente.")]
    [SwaggerResponse(200, "Funcionário atualizado com sucesso")]
    [SwaggerResponse(400, "Dados inválidos")]
    public async Task<IActionResult> Put([FromBody] EmployeeDto employeeDto)
    {
        var employee = new Employee(employeeDto.Name, employeeDto.Email, employeeDto.YardId, employeeDto.Password);
        await _update.ExecuteAsync(employee);
        return Ok();
    }

    [HttpDelete("{email}")]
    [SwaggerOperation(Summary = "Remove um funcionário", Description = "Deleta o funcionário correspondente ao email fornecido.")]
    [SwaggerResponse(204, "Funcionário removido com sucesso")]
    [SwaggerResponse(404, "Funcionário não encontrado")]
    public async Task<IActionResult> Delete([FromRoute] string email)
    {
        await _delete.ExecuteAsync(email);
        return NoContent();
    }
}
