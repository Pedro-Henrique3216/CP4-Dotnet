using MottuChallenge.Application.Helpers;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.UseCases.Spots;

public class SpotUseCase
{
    public List<Spot> GenerateSpot(Sector sector, double width, double height)
    {
        var spots = new List<Spot>();

        var minX = sector.Points.Min(p => p.X);
        var maxX = sector.Points.Max(p => p.X); 
        var minY = sector.Points.Min(p => p.Y); 
        var maxY = sector.Points.Max(p => p.Y);
        
        var y = minY;

        while (y <= maxY)
        {
            var x = minX;
            while (x <= maxX)
            {
                if (GeometryHelper.IsPointInsidePolygon(x, y, sector.Points.ToList()))
                {
                    spots.Add(new Spot(x, y, sector.Id));
                }
                x += width;
            }
            y += height;
        }

        return spots;
    }
}