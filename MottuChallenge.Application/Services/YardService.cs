using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.Services
{
    public class YardService(IYardRepository yardRepository, IAddressService addressService) : IYardService
    {
        public async Task<Yard> SaveYardAsync(CreateYardDto createYardDto)
        {
            var address = await addressService.GetAddressByCepAsync(createYardDto.Cep, createYardDto.Number);

            var yard = new Yard(createYardDto.Name, address.Id)
            {
                Address = address
            };

            foreach (var pointDto in createYardDto.Points)
            {
                var point = new PolygonPoint(pointDto.PointOrder, pointDto.X, pointDto.Y);
                yard.AddPoint(point);
            }
            
            await yardRepository.SaveYardAsync(yard);
            return yard;
        }

        public async Task<YardResponseDto?> GetYardResponseByIdAsync(Guid id)
        {
            var yard = await yardRepository.GetYardByIdAsync(id);
            var address = await addressService.GetAddressByIdAsync(yard.AddressId);
            yard.Address = address;

            var addressResponse = createAddressResponseDto(yard.Address);
            var points = createListOfPointResponseDto(yard);
            return new YardResponseDto { Address = addressResponse, Id = yard.Id, Name = yard.Name, Points = points };
        }

        public async Task<Yard?> GetYardByIdAsync(Guid id)
        {
            var yard = await yardRepository.GetYardByIdAsync(id);
            var address = await addressService.GetAddressByIdAsync(yard.AddressId);
            yard.Address = address;

            return yard;
        }

        public async Task<List<YardResponseDto>> GetAllYardsAsync()
        {
            var yards = await yardRepository.GetAllYardsAsync();

            var yardsResponse = new List<YardResponseDto>();
            foreach (var yard in yards) 
            {
                var address = await addressService.GetAddressByIdAsync(yard.AddressId);
                yard.Address = address;
                var addressResponse = createAddressResponseDto(yard.Address);
                var points = createListOfPointResponseDto(yard);
                yardsResponse.Add(new YardResponseDto { Address = addressResponse, Id = yard.Id, Name = yard.Name, Points = points });
            }
            return yardsResponse;
        }

        private AddressResponseDto createAddressResponseDto(Address address)
        {
            return new AddressResponseDto { City = address.City, Country = address.Country, Neighborhood = address.Neighborhood, Number = address.Number, State = address.State, Street = address.Street, ZipCode = address.ZipCode };
        }

        private List<PointResponseDto> createListOfPointResponseDto(Yard yard)
        {
            var points = new List<PointResponseDto>();

            foreach (var point in yard.Points)
            {
                points.Add(new PointResponseDto { PointOrder = point.PointOrder, X = point.X, Y = point.Y });
            }

            return points;
        }

    }
}
