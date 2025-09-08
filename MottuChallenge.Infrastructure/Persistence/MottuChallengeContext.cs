using Microsoft.EntityFrameworkCore;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Persistence
{
    internal class MottuChallengeContext(DbContextOptions<MottuChallengeContext> options) : DbContext(options)
    {
        public DbSet<Yard> Yards { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
