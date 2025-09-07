namespace MottuChallenge.Domain.Entities
{
    public class Yard
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid AddressId { get; private set; }
        public Address Address { get; private set; }

        public Yard(string name, Guid addressId)
        {
            
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.AddressId = addressId;
        }

        public Yard() { }

    }
}
