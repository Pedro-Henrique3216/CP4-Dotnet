using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;
using System.Text.Json;

namespace MottuChallenge.Application.Services
{
    public class AddressService(HttpClient httpClient, IAddressRepository addressRepository) : IAddressService
    {
        public async Task<Address> GetAddressByCepAsync(string cep, string number)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json/";
            var response = await httpClient.GetStringAsync(url);

            var viacepResponse = JsonSerializer.Deserialize<ViaCepResponse>(response)
                                 ?? throw new Exception("CEP não encontrado");

            return new Address
            (
                viacepResponse.Logradouro,
                int.Parse(number),
                viacepResponse.Bairro,
                viacepResponse.Localidade,
                viacepResponse.Uf,
                cep,
                "Brasil"
            );
        }

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await addressRepository.GetAddressByIdAsync(id);
        }
    }
}
