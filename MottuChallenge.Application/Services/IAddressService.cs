using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface IAddressService
    {
        Task<Address> GetAddressByCepAsync(string cep, string number);

        Task<Address> GetAddressByIdAsync(Guid id);
    }
}
