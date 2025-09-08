using Microsoft.EntityFrameworkCore;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Mapping;

namespace MottuChallenge.Infrastructure.Persistence
{
    internal class MottuChallengeContext(DbContextOptions<MottuChallengeContext> options) : DbContext(options)
    {
        public DbSet<Yard> Yards { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new YardMapping());
        }
    }
}
