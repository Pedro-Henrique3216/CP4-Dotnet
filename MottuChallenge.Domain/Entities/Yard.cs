using MottuChallenge.Domain.Validations;
using MottuChallenge.Domain.ValueObjects;

namespace MottuChallenge.Domain.Entities
{
    public class Yard
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid AddressId { get; private set; }
        public Address Address { get; set; }

        private readonly List<Sector> _sectors = new();
        public IReadOnlyCollection<Sector> Sectors => _sectors.AsReadOnly();

        private readonly List<PolygonPoint> _points = new();
        public IReadOnlyCollection<PolygonPoint> Points => _points.AsReadOnly();

        public Yard(string name, Guid addressId)
        {
            Guard.AgainstNullOrWhitespace(name, nameof(name), nameof(Yard));
            Guard.AgainstNullOrEmpty(addressId, nameof(addressId), nameof(Yard));
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.AddressId = addressId;
        }

        public Yard() { }

        public void AddSector(Sector sector)
        {
            _sectors.Add(sector);
        }

        public void AddPoint(PolygonPoint point)
        {
            _points.Add(point);
        }
    }
}
