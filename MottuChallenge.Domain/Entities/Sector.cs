using MottuChallenge.Domain.Validations;
using MottuChallenge.Domain.ValueObjects;

namespace MottuChallenge.Domain.Entities
{
    public class Sector
    {
        public Guid Id { get; private set; }
        public Guid YardId { get; private set; }
        public Yard Yard { get; private set; }
        public SectorType SectorType { get; private set; }
        public Guid SectorTypeId { get; private set; }

        private readonly List<PolygonPoint> _points = new();
        public IReadOnlyCollection<PolygonPoint> Points => _points.AsReadOnly();

        private readonly List<Spot> _spots = new();
        public IReadOnlyCollection<Spot> Spots => _spots.AsReadOnly();

        public Sector(Guid sectorTypeId, Guid yardId)
        {
            Guard.AgainstNullOrEmpty(sectorTypeId, nameof(sectorTypeId), nameof(Sector));
            Guard.AgainstNullOrEmpty(yardId, nameof(yardId), nameof(Sector));
            this.Id = Guid.NewGuid();
            this.SectorTypeId = sectorTypeId;
            this.YardId = yardId;
        }

        private Sector() { }

        public void AddSpot(Spot spot) => _spots.Add(spot);
        public void AddPoint(PolygonPoint point) => _points.Add(point);
    }
}
