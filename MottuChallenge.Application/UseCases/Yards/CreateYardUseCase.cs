using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Interfaces;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Yards;

public class CreateYardUseCase
{
    private readonly IYardRepository _yardRepository;
    private readonly IAddressProvider _addressProvider;

    public CreateYardUseCase(IYardRepository yardRepository, IAddressProvider addressProvider)
    {
        _yardRepository = yardRepository;
        _addressProvider = addressProvider;
    }

    public async Task<Yard> ExecuteAsync(CreateYardDto createYardDto)
    {
        if (createYardDto is null)
            throw new ArgumentNullException(nameof(createYardDto));

        var viaCep =  await _addressProvider.GetAddressByCepAsync(createYardDto.Cep, createYardDto.Number);
        var address = new Address(
            viaCep.Street,
            int.Parse(createYardDto.Number),
            viaCep.Neighborhood,
            viaCep.City,
            viaCep.State,
            createYardDto.Cep,
            viaCep.Country
        );
        
        var yard = new Yard(createYardDto.Name, address.Id)
        {
            Address = address
        };

        foreach (var pointDto in createYardDto.Points)
        {
            var point = new PolygonPoint(pointDto.PointOrder, pointDto.X, pointDto.Y);
            yard.AddPoint(point);
        }

        await _yardRepository.SaveYardAsync(yard);
        return yard;
    }
}