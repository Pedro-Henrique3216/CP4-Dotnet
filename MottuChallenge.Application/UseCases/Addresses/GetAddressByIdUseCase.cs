using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Addresses;

public class GetAddressByIdUseCase
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressByIdUseCase(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Address> ExecuteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("O ID do endereço é inválido.", nameof(id));

        var address = await _addressRepository.GetAddressByIdAsync(id);

        if (address == null)
            throw new Exception("Endereço não encontrado.");

        return address;
    }
}