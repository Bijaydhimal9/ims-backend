using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models.RequestModels;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Common;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations
{
    public static class ServiceConfigurationExtensions
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion);
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 12;
            }).AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddHostedService<IdentityDataSeeder>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IInmateProfileService, InmateProfileService>();
            services.AddTransient<IBookingService, BookingService>();

            services.AddSingleton<IValidator<LoginRequestModel>, LoginValidator>();
            services.AddSingleton<IValidator<InmateProfileRequestModel>, InmateProfileValidator>();
            services.AddSingleton<IValidator<BookingRequestModel>, BookingValidator>();




            return services;
        }
    }
}