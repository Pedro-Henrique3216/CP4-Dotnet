using MottuChallenge.Domain.Enums;
using MottuChallenge.Domain.Validations;

namespace MottuChallenge.Domain.Entities
{
    public class Spot
    {
        public Guid SpotId { get; private set; }
        public Guid SectorId { get; private set; }
        public Sector Sector { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public SpotStatus Status { get; private set; }
        public Motorcycle? Motorcycle { get; private set; }
        public Guid? MotorcycleId { get; set; }

        public Spot(double x, double y, Guid sectorId)
        {
            Guard.AgainstNegativeCoordinates(x, y, nameof(Spot));
            Guard.AgainstNullOrEmpty(sectorId, nameof(sectorId), nameof(Spot));
            this.SectorId = sectorId;
            this.SpotId = Guid.NewGuid();
            this.X = x;
            this.Y = y;
            this.Status = SpotStatus.FREE;

        }
        public Spot() { }
    }
}
