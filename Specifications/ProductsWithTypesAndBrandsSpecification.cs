using ECommerce_App.Models;
using System.Linq.Expressions;

namespace ECommerce_App.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

        }
    }
}
