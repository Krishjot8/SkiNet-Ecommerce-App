﻿using ECommerce_App.Data;
using ECommerce_App.Identity;
using ECommerce_App.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce_App.Extensions
{
    public static class IdentityServiceExtensions
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config)
        
        
        {

            services.AddDbContext<AppIdentityDbContext>(options =>
            {

                options.UseSqlServer(config.GetConnectionString("IdentityConnection"));


            });

            services.AddIdentityCore<AppUser>(options =>
            {

                // Add Identity Options Here


            })

            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = false,
                      ValidateAudience = false

                    };




                });


            services.AddAuthorization();

            return services;

        }


    }
}
