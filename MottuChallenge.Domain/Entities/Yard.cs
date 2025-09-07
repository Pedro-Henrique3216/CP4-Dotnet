using MottuChallenge.Domain.Validations;

namespace MottuChallenge.Domain.Entities
{
    public class Yard
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid AddressId { get; private set; }
        public Address Address { get; private set; }
        public ICollection<Sector> Sectors { get; private set; } = new List<Sector>();

        public Yard(string name, Guid addressId)
        {
            Guard.AgainstNullOrWhitespace(name, nameof(name), nameof(Yard));
            Guard.AgainstNullOrEmpty(addressId, nameof(addressId), nameof(Yard));
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.AddressId = addressId;
        }

        public Yard() { }

    }
}
