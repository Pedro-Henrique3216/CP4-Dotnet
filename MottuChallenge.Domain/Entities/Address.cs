using MottuChallenge.Domain.Exceptions;

namespace MottuChallenge.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }

        public Address(string street, int number, string neighborhood, string city, string state, string zipCode, string country)
        {
            ValidateNumber(number);
            ValidateZipCode(zipCode);
            validateStreet(street);
            this.Id = Guid.NewGuid();
            this.Street = street;
            this.Number = number;
            this.Neighborhood = neighborhood;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
            this.Country = country;
        }
        public Address() { }

        private void ValidateNumber(int number)
        {
            if (number < 0)
            {
                throw new DomainValidationException("Number must be non-negative", nameof(number), nameof(Address));
            }

        }

        private void validateStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new DomainValidationException("Street is required", nameof(street), nameof(Address));
            }
        }   

        private void ValidateZipCode(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new DomainValidationException("Zip code is required", nameof(zipCode), nameof(Address));
            }
            if (zipCode.Length != 8)
            {
                throw new DomainValidationException("Zip code must have 8 characters", nameof(zipCode), nameof(Address));
            }
        }
    }
}
