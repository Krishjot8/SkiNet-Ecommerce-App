using Azure.Identity;
using ECommerce_App.Data;
using ECommerce_App.Errors;
using ECommerce_App.Extensions;
using ECommerce_App.Identity;
using ECommerce_App.Middleware;
using ECommerce_App.Models.Identity;
using ECommerce_App.Repositories;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Identity;
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
        builder.Services.AddIdentityServices(builder.Configuration);
        builder.Services.AddSwaggerDocumentation();

        var app = builder.Build();


        app.UseMiddleware<ExceptionMiddleware>();

        // Configure the HTTP request pipeline.


        app.UseStatusCodePagesWithReExecute("/errors/{0}");


        app.UseSwaggerDocumentation();

        app.UseStaticFiles();

        app.UseCors("CorsPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();
        var identityContext = services.GetRequiredService<AppIdentityDbContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var logger = services.GetRequiredService<ILogger<Program>>();


        try { 
        await context.Database.MigrateAsync();
            await identityContext.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
            await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
        }
      catch(Exception ex)
        {

            logger.LogError(ex,"An error occured during migration");

        }

        app.Run();
    }
}