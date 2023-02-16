using ECommerce_App.Data;
using ECommerce_App.Models;
using ECommerce_App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]

        public async Task<ActionResult <List<Product>>> GetProducts()
        {


            var products = await _repository.GetProductsAsync();

            return Ok (products);

        }



        [HttpGet("{id}")]


        public async Task <ActionResult<Product>> GetProduct(int id)
        {


            return await _repository.GetProductByIdAsync(id);
        
        }


        [HttpGet("brands")]

        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {


            var productbrands = await _repository.GetProductBrandsAsync();

            return Ok(productbrands);

        }


        [HttpGet("brands/{id}")]


        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {


            return await _repository.GetProductBrandByIdAsync(id);

        }

        [HttpGet("types")]

        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {


            var producttypes = await _repository.GetProductTypesAsync();

            return Ok(producttypes);

        }


        [HttpGet("types/{id}")]


        public async Task<ActionResult<ProductType>> GetProductType(int id)
        {


            return await _repository.GetProductTypesByIdAsync(id);

        }


        [HttpPost]


        public string AddProduct()
        {

            return "This adds a product";


        }


        [HttpPut]


        public string UpdateProductsByID()
        {

            return "This updates a product";


        }


        [HttpDelete]


        public string DeleteProductsById()
        {

            return "This deletes a product";


        }

    }
}
