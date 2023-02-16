using ECommerce_App.Models;

namespace ECommerce_App.Repositories
{
    public interface IProductRepository
    {

        

        Task<List<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<List<ProductBrand>> GetProductBrandsAsync();

        Task<ProductBrand> GetProductBrandByIdAsync(int id);

        Task<List<ProductType>> GetProductTypesAsync();

        Task<ProductType> GetProductTypesByIdAsync(int id);
    }
}
