
using System;
using System.Linq;
using System.Threading.Tasks;
using Hydra.Core.Data;
using Hydra.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Hydra.Core.Messages;
using Hydra.Core.Communication.Mediator;
using Hydra.Core.Extensions;
using FluentValidation.Results;

namespace Hydra.Customers.Infrastructure.Data
{
    public sealed class CustomersContext: DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        //DbContextOptions used for entity framework core in dotnet core.
        //It is a kind of factory that will be configure the context on Startup.cs
        public CustomersContext(DbContextOptions<CustomersContext> options, IMediatorHandler mediatorHandler) : base(options){ 
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Customer> Customers {get; set; }
        public DbSet<Address> Addresses {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersContext).Assembly);
        }

        public async Task<bool> Commit() {
            var result = await base.SaveChangesAsync() > 0;
            if(result) await _mediatorHandler.PublishEvents(this);

            return result;
        }
    }
}