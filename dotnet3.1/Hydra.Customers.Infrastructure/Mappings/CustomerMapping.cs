using Hydra.Core.DomainObjects;
using Hydra.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Customers.Infrastructure.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.Property(c => c.IdentityNumber)
                   .IsRequired()
                   .HasColumnType("varchar(50)");
            
            builder.OwnsOne(c => c.Email, ba => {
                ba.Property(c => c.Value)
                  .IsRequired()
                  .HasColumnName("Email")
                  .HasColumnType($"varchar({Email.MaxLength})");
            });

            // 1 : 1 => Customer : Address
            builder.HasOne(c => c.Address)
                    .WithOne(c => c.Customer);

            builder.ToTable("Customer");
        }
    }
}