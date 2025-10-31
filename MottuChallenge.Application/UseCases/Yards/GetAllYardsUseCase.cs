using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Yards;

public class GetAllYardsUseCase
{
    private readonly IYardRepository _yardRepository;
    private readonly IAddressRepository _addressRepository;

    public GetAllYardsUseCase(IYardRepository yardRepository, IAddressRepository addressRepository)
    {
        _yardRepository = yardRepository;
        _addressRepository = addressRepository;
    }

    public async Task<List<YardResponseDto>> ExecuteAsync()
    {
        var yards = await _yardRepository.GetAllYardsAsync();
        var result = new List<YardResponseDto>();

        foreach (var yard in yards)
        {
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

            result.Add(new YardResponseDto
            {
                Id = yard.Id,
                Name = yard.Name,
                Address = addressResponse,
                Points = points
            });
        }

        return result;
    }
}