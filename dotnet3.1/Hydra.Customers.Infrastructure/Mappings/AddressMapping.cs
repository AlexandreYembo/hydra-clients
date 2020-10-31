using Hydra.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Customers.Infrastructure.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Street)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

            builder.Property(c => c.Number)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            builder.Property(c => c.State)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
        
            builder.Property(c => c.Country)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

            builder.Property(c => c.PostCode)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

            builder.ToTable("Address");
        }
    }
}