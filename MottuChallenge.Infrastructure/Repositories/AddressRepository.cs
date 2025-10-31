using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class AddressRepository(MottuChallengeContext context) : IAddressRepository
    {
        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await context.Addresses.FindAsync(id);
        }
    }
}
