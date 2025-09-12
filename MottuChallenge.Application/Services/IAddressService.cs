using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface IAddressService
    {
        public Task<Address> GetAddressByCepAsync(string cep, string number);

        public Task<Address> GetAddressByIdAsync(Guid id);
    }
}
