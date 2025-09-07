using MottuChallenge.Domain.Exceptions;
using MottuChallenge.Domain.Validations;

namespace MottuChallenge.Domain.Entities
{
    public class PolygonPoint
    {
        public int Id { get; private set; }
        public Guid? YardId { get; private set; }
        public Guid? SectorId { get; private set; }
        public int PointOrder { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public Yard? Yard { get; private set; }
        public Sector? Sector { get; private set; }

        public PolygonPoint(int pointOrder, double x, double y, Guid yardId, Guid sectorId)
        {
            ValidatePointOrder(pointOrder);
            Guard.AgainstNegativeCoordinates(x, y, nameof(PolygonPoint));   
            this.PointOrder = pointOrder;
            this.X = x;
            this.Y = y;
            this.YardId = yardId != Guid.Empty ? yardId : null;
            this.SectorId = sectorId != Guid.Empty ? sectorId : null;
        }

        public PolygonPoint() { }

        private void ValidatePointOrder(int pointOrder)
        {
            if (pointOrder < 0)
            {
                throw new DomainValidationException("Point order must be non-negative", nameof(pointOrder), nameof(PolygonPoint));
            }
        }
    
    }
}
