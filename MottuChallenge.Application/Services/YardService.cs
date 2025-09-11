using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.Services
{
    public class YardService(IYardRepository yardRepository, IAddressService addressService) : IYardService
    {

        private readonly IYardRepository _yardRepository = yardRepository;
        private readonly IAddressService _addressService = addressService;

        public async Task<Yard> SaveYardAsync(CreateYardDto createYardDto)
        {
            var address = await _addressService.GetAddressByCepAsync(createYardDto.Cep, createYardDto.Number);

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

        public async Task<Yard?> GetYardByIdAsync(Guid id)
        {
            return await _yardRepository.GetYardByIdAsync(id);
        }
    }
}
