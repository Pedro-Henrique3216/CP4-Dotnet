using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Employees;

public class GetEmployeeByEmailUseCase
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByEmailUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Employee?> ExecuteAsync(string email)
    {
        return await _repository.GetUserByEmailAsync(email);
    }
}