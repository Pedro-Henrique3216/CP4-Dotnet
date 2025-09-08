using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Mapping
{
    internal class YardMapping : IEntityTypeConfiguration<Yard>
    {
        public void Configure(EntityTypeBuilder<Yard> builder)
        {
            builder.ToTable("yards");

            builder.HasKey(y => y.Id);

            builder.Property(y => y.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(y => y.Name)
                   .HasColumnName("name")
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(y => y.AddressId)
                   .HasColumnName("address_id")
                   .IsRequired();

            builder.HasOne(y => y.Address)
                   .WithOne()
                   .HasForeignKey<Yard>(y => y.AddressId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(y => y.Sectors)
                   .WithOne(s => s.Yard)
                   .HasForeignKey(s => s.YardId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(y => y.Points)
                   .WithOne(p => p.Yard)
                   .HasForeignKey(p => p.YardId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
