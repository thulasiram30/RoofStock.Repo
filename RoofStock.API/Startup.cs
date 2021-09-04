using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RoofStock.Data.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using AutoMapper;
using RoofStock.Data.AutoMapper;
using RoofStock.Data.Contracts;
using RoofStock.Data;
using System.Net.Http;
using RoofStock.Data.SeedData;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using RoofStock.API.Middleware;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace RoofStock.API
{
    public class Startup
    {
        readonly string crosOriginPolicy= "AllowOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
                      
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddMaps(AutoMapperProfiles.GetAutoMapperProfiles());
            }).CreateMapper();
            services.AddSingleton(mapper);
            var connectionString = Configuration["ConnectionStrings:RoofStockDB"];
            services.AddScoped<HttpClient>();
            services.AddScoped<IRoofStockSeedData, RoofStockSeedData>();
            services.AddHealthChecks().AddDbContextCheck<RoofStockDBContext>();
            services.AddDbContext<RoofStockDBContext>(opts => opts.UseSqlServer(connectionString,
                        optionBuilder => optionBuilder.EnableRetryOnFailure()));
            services.AddScoped<IAddressData, AddressData>();
            services.AddScoped<IPropertyData, PropertyData>();
            services.AddControllers();
            services.AddCors(options => {
                options.AddPolicy(name: crosOriginPolicy, bulider => {
                    bulider.WithOrigins("http://localhost:4200", "https://localhost:44348")
                    .AllowAnyHeader()
                     .AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RoofStock.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoofStock.API v1"));
            }
            
            app.UseMiddleware<ExceptionMiddleware>();
            UpdateMigrationWithSeedData(app);
            app.UseHttpsRedirection();            
            app.UseRouting();
            app.UseCors(crosOriginPolicy);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });
        }

        public void UpdateMigrationWithSeedData(IApplicationBuilder app)
        {
            Task.Run(async () =>
            {
                using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
                using var context = serviceScope.ServiceProvider.GetService<RoofStockDBContext>();
                context.Database.Migrate();
                if (!context.Properties.Any()) {
                    var seedContext = serviceScope.ServiceProvider.GetService<IRoofStockSeedData>();
                    var seedData = seedContext.SeedData();
                    context.Properties.AddRange(seedData);
                    await context.SaveChangesAsync();
                }
            });
        }

        public CorsPolicy GenerateCorsPolicy()
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
                                          //corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
            return corsBuilder.Build();
        }
    }
   
}
