using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using UxComexTest.Api.Models;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Interfaces.Services;
using UxComexTest.Domain.Repositories;
using UxComexTest.Domain.Services;
using UxComexTest.Infra.Repositories;

namespace UxComexTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "All",
                                  builder =>
                                  {
                                      builder.WithOrigins("*");
                                  });
            });

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo()
                    {
                        Title = "UxComex Test - Aline Oliveira",
                        Version = "v1",
                        Description = "UxComex Test - Aline Oliveira",
                        Contact = new OpenApiContact
                        {
                            Name = "Aline Oliveira",
                            Url = new Uri("https://github.com/alineprudenciano")
                        }
                    });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressService, AddressService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressModel>();
                cfg.CreateMap<AddressModel, Address>();
                
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();

                cfg.CreateMap<BaseEntity, BaseEntity>();
                cfg.CreateMap<BaseEntity, BaseEntity>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var dbConnection = new SqlConnection(Configuration.GetConnectionString("UxComexTest"));

            services.AddSingleton<IDbConnection>(dbConnection);

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("All");

            app.UseSwagger();
            
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UxComex Test - Aline Oliveira");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
