using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Repositories
{
    public interface IYardRepository
    {
        Task<Yard> SaveYardAsync(Yard yard);

        Task<Yard?> GetYardByIdAsync(Guid id);

        public Task<List<Yard>> GetAllYardsAsync();

    }
}
