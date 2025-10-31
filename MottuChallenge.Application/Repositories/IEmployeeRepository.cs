using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> GetUserByEmailAsync(string email);
    
    Task<List<Employee>> GetUsersAsync();
    
    Task AddUserAsync(Employee user);
    
    Task UpdateUserAsync(Employee user);
    
    Task DeleteUserAsync(Employee user);
}