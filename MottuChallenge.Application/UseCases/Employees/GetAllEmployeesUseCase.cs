using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Employees;

public class GetAllEmployeesUseCase
{
    private readonly IEmployeeRepository _repository;

    public GetAllEmployeesUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Employee>> ExecuteAsync()
    {
        return await _repository.GetUsersAsync();
    }
}