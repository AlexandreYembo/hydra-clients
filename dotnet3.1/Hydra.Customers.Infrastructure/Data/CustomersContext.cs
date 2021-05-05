using System.Linq;
using System.Threading.Tasks;
using Hydra.Core.Data.Context;
using Hydra.Core.Mediator.Abstractions.Mediator;
using Hydra.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Customers.Infrastructure.Data
{
    public sealed class CustomersContext: HydraDbContext//, IUnitOfWork
    {
        //DbContextOptions used for entity framework core in dotnet core.
        //It is a kind of factory that will be configure the context on Startup.cs
        public CustomersContext(DbContextOptions<CustomersContext> options, IMediatorHandler mediatorHandler) : base(options, mediatorHandler){ 
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Customer> Customers {get; set; }
        public DbSet<Address> Addresses {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersContext).Assembly);
        }

        public override async Task<bool> Commit() {
            return await base.Commit();
        }
    }
}