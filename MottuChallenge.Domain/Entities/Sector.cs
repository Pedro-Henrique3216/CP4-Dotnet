using MottuChallenge.Domain.Validations;

namespace MottuChallenge.Domain.Entities
{
    public class Sector
    {
        public Guid Id { get; private set; }
        public Guid YardId { get; private set; }
        public Yard Yard { get; private set; }
        public SectorType SectorType { get; private set; }
        public Guid SectorTypeId { get; private set; }
       
        public Sector(Guid sectorTypeId, Guid yardId)
        {
            Guard.AgainstNullOrEmpty(sectorTypeId, nameof(sectorTypeId), nameof(Sector));
            Guard.AgainstNullOrEmpty(yardId, nameof(yardId), nameof(Sector));
            this.Id = Guid.NewGuid();
            this.SectorTypeId = sectorTypeId;
            this.YardId = yardId;
        }

        public Sector() { }

    }
}
