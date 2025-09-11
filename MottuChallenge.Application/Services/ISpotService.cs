using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface ISpotService
    {

        public List<Spot> GenerateSpot(Sector sector, double width, double height);
    }
}
