using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface IYardService
    {
        Task<Yard> SaveYardAsync(CreateYardDto createYardDto);

        Task<Yard?> GetYardByIdAsync(Guid id);
    }
}
