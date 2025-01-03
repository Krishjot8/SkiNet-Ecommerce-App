﻿using Microsoft.OpenApi.Models;

namespace ECommerce_App.Extensions
{
    public static class SwaggerServiceExtensions

    {

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>

            {

                var securitySchema = new OpenApiSecurityScheme()
                {
                    Description = " JWT Auth Bearer Scheme",
                    Name =  "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"

                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {


                        securitySchema, new[] {"Bearer"}
                    }

                };

                c.AddSecurityRequirement(securityRequirement);
            });

            return services;
        
        
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
            });

            return app;

        }

    }
}
