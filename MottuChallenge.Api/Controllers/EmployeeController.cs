using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.Employees;
using MottuChallenge.Domain.Entities;

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
    public async Task<IActionResult> Post([FromBody] EmployeeDto employeeDto)
    {
        var employee = new Employee(employeeDto.Name, employeeDto.Email, employeeDto.YardId, employeeDto.Password);
        await _create.ExecuteAsync(employee);
        return Created("", null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _getAll.ExecuteAsync();
        return Ok(employees);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetByEmail([FromRoute] string email)
    {
        var employee = await _getByEmail.ExecuteAsync(email);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] EmployeeDto employeeDto)
    {
        var employee = new Employee(employeeDto.Name, employeeDto.Email, employeeDto.YardId, employeeDto.Password);
        await _update.ExecuteAsync(employee);
        return Ok();
    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> Delete([FromRoute] string email)
    {
        await _delete.ExecuteAsync(email);
        return NoContent();
    }
}