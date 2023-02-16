using Azure.Identity;
using ECommerce_App.Data;
using ECommerce_App.Repositories;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

internal class Program
{
   
    private static async   Task Main(string[] args) 

    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.


        builder.Services.AddScoped<IProductRepository,ProductRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<StoreContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



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