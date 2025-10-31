using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Yards;

public class GetYardByIdUseCase
{
    private readonly IYardRepository _yardRepository;
    private readonly IAddressRepository _addressRepository;

    public GetYardByIdUseCase(IYardRepository yardRepository, IAddressRepository addressRepository)
    {
        _yardRepository = yardRepository;
        _addressRepository = addressRepository;
    }

    public async Task<YardResponseDto?> ExecuteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("ID inválido.", nameof(id));

        var yard = await _yardRepository.GetYardByIdAsync(id);
        if (yard == null)
            return null;

        var address = await _addressRepository.GetAddressByIdAsync(yard.AddressId);
        yard.Address = address;

        var addressResponse = new AddressResponseDto
        {
            City = address.City,
            Country = address.Country,
            Neighborhood = address.Neighborhood,
            Number = address.Number,
            State = address.State,
            Street = address.Street,
            ZipCode = address.ZipCode
        };

        var points = yard.Points.Select(p => new PointResponseDto
        {
            PointOrder = p.PointOrder,
            X = p.X,
            Y = p.Y
        }).ToList();

        return new YardResponseDto
        {
            Id = yard.Id,
            Name = yard.Name,
            Address = addressResponse,
            Points = points
        };
    }
}