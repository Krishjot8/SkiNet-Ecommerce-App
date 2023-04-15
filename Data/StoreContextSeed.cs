using ECommerce_App.Models;
using ECommerce_App.Models.OrderAggregate;
using System.Text.Json;

namespace ECommerce_App.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context) 
        
        
        {

            if (!context.ProductBrands.Any())
            {


                var brandsData = File.ReadAllText("/Udemy/Neil Cummings/eCommerce App/ECommerce App/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);

            }
            if (!context.ProductTypes.Any())
            {


                var typesData = File.ReadAllText("/Udemy/Neil Cummings/eCommerce App/eCommerceApp-API/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);

            }
            if (!context.Products.Any())
            {


                var productsData = File.ReadAllText("/Udemy/Neil Cummings/eCommerce App/eCommerceApp-API/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);

            }

            if (!context.DeliveryMethods.Any())
            {


                var deliveryData = File.ReadAllText("/Udemy/Neil Cummings/eCommerce App/eCommerceApp-API/Data/SeedData/deliveries.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(methods);

            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();// this will save the changes in the database





        }
    }
}
