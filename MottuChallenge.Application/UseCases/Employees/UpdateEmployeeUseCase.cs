using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Employees;

public class UpdateEmployeeUseCase
{
    private readonly IEmployeeRepository _repository;

    public UpdateEmployeeUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Employee employee)
    {
        var existing = await _repository.GetUserByEmailAsync(employee.Email);
        if (existing == null)
            throw new KeyNotFoundException("Employee not found.");

        await _repository.UpdateUserAsync(employee);
    }
}