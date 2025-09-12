using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Repositories
{
    public interface IAddressRepository
    {

        public Task<Address> GetAddressByIdAsync(Guid id);
    }
}
