
using System;
using System.Linq;
using System.Threading.Tasks;
using Hydra.Core.Data;
using Hydra.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Hydra.Core.Messages;
using Hydra.Core.Communication.Mediator;
using Hydra.Core.Extensions;

namespace Hydra.Customers.Infrastructure.Data
{
    public class CustomersContext: DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        //DbContextOptions used for entity framework core in dotnet core.
        //It is a kind of factory that will be configure the context on Startup.cs
        public CustomersContext(DbContextOptions<CustomersContext> options, IMediatorHandler mediatorHandler) : base(options){ 
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Customer> Customers {get; set; }
        public DbSet<Address> Addresses {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)"); // If I forgot to map any context, it will avoid do create any column NVarchar(MAX)

            //when I remove the customer, it won't remove the relationship with the customer
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                                            .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.Ignore<Event>();
                //Does not need to add map for each element, new EF supports
                //It will find all entities and mapping defined on DbSet<TEntity> via reflection
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit() {
            var result = await base.SaveChangesAsync() > 0;
            if(result) await _mediatorHandler.PublishEvents(this);

            return result;
        }
    }
}