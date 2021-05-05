using AutoMapper;
using Hydra.Core.API.Identity;
using Hydra.Customers.API.AutoMapper;
using Hydra.Customers.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hydra.Customers.API.Setup
{
   public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomersContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(options =>{
                options.AddPolicy("Customer", builder =>
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
            
            //Package: AutoMapper.Extensions.Microsoft.DependencyInjection - used to work with  native Aspnet core dependency injection
            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
            //MediatR.Extensions.Microsoft.DependencyInjection  - used to work with  native Aspnet core dependency injection
            services.AddMediatR(typeof(Startup));
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Customer");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}