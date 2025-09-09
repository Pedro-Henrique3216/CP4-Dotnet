using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    internal interface IAddressService
    {
        public Task<Address> GetAddressByCepAsync(string cep, string number);
    }
}
