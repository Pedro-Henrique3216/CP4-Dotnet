using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Employees;

public class CreateEmployeeUseCase
{
    private readonly IEmployeeRepository _repository;

    public CreateEmployeeUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Employee employee)
    {
        var existing = await _repository.GetUserByEmailAsync(employee.Email);
        if (existing != null)
            throw new InvalidOperationException("Employee with this email already exists.");
        
        await _repository.AddUserAsync(employee);
    }
}