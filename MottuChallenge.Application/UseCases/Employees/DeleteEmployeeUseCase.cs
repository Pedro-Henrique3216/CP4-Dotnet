using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Employees;

public class DeleteEmployeeUseCase
{
    private readonly IEmployeeRepository _repository;

    public DeleteEmployeeUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(string email)
    {
        var existing = await _repository.GetUserByEmailAsync(email);
        if (existing == null)
            throw new KeyNotFoundException("Employee not found.");

        await _repository.DeleteUserAsync(existing);
    }
}