using MongoDB.Driver;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories;

public class EmployeeRepository(MongoContext context) : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> _collection = context.Database.GetCollection<Employee>("UsersPJ");

    public Task<Employee> GetUserByEmailAsync(string email)
    {
        return _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public Task<List<Employee>> GetUsersAsync()
    {
        return  _collection.Find(u => true).ToListAsync();
    }

    public async Task AddUserAsync(Employee user)
    {
        await _collection.InsertOneAsync(user);
    }

    public async Task UpdateUserAsync(Employee user)
    {
        await _collection.ReplaceOneAsync(u => u.Email == user.Email, user);
    }

    public async Task DeleteUserAsync(Employee user)
    {
        await _collection.DeleteOneAsync(u => u.Email == user.Email);
    }
}