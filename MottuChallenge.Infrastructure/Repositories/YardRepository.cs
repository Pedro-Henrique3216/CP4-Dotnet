using Microsoft.EntityFrameworkCore;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class YardRepository(MottuChallengeContext context) : IYardRepository
    {
        public async Task<Yard> SaveYardAsync(Yard yard)
        {
           await context.Yards.AddAsync(yard);
           await context.SaveChangesAsync();
           return yard;
        }

        public async Task<Yard?> GetYardByIdAsync(Guid id)
        {
            return await context.Yards.FindAsync(id);
        }

        public async Task<List<Yard>> GetAllYardsAsync()
        {
            return await context.Yards.ToListAsync();
        }
    }
}
