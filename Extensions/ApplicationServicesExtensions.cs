using ECommerce_App.Data;
using ECommerce_App.Errors;
using ECommerce_App.Repositories;
using ECommerce_App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace ECommerce_App.Extensions
{
    public static class ApplicationServicesExtensions
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config ) 
        
        {
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {

                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork , UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<ApiBehaviorOptions>(options =>

            {

                options.InvalidModelStateResponseFactory = actionContext =>

                {

                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse

                    {
                        Errors = errors

                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

          

            services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddCors(opt =>

            {

                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");

                });


            });

            return services;
        }
    }
}
