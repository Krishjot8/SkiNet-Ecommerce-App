using ECommerce_App.Data;
using ECommerce_App.Errors;
using ECommerce_App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_App.Extensions
{
    public static class ApplicationServicesExtensions
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config ) 
        
        {

            services.AddScoped<IProductRepository, ProductRepository>();
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

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddCors(opt =>

            {

                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");

                });


            });

            return services;
        }
    }
}
