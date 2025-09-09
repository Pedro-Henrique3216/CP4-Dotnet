using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class YardRepository(MottuChallengeContext context) : IYardRepository
    {
        private readonly MottuChallengeContext _context = context;

        public async Task<Yard> SaveYardAsync(Yard yard)
        {
           await _context.Yards.AddAsync(yard);
           await _context.SaveChangesAsync();
           return yard;
        }
    }
}
