using Azure.Identity;
using ECommerce_App.Data;
using ECommerce_App.Errors;
using ECommerce_App.Extensions;
using ECommerce_App.Middleware;
using ECommerce_App.Repositories;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

internal class Program
{
   
    private static async   Task Main(string[] args) 

    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddApplicationServices(builder.Configuration);

       var app = builder.Build();


        app.UseMiddleware<ExceptionMiddleware>();

        // Configure the HTTP request pipeline.


        app.UseStatusCodePagesWithReExecute("/errors/{0}");



        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
            });
        }

        app.UseStaticFiles();

        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();  
        var logger = services.GetRequiredService<ILogger<Program>>();


        try { 
        await context.Database.MigrateAsync();
           await StoreContextSeed.SeedAsync(context);
        }
      catch(Exception ex)
        {

            logger.LogError(ex,"An error occured during migration");

        }

        app.Run();
    }
}