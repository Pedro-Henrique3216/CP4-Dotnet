using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface IYardService
    {
        Task<Yard> SaveYardAsync(CreateYardDto createYardDto);

        Task<Yard?> GetYardByIdAsync(Guid id);

        Task<List<YardResponseDto>> GetAllYardsAsync();

        Task<YardResponseDto?> GetYardResponseByIdAsync(Guid id);
    }
}
