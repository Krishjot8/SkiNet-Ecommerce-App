using ECommerce_App.Data;
using ECommerce_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]


        public async Task<ActionResult <List<Product>>> GetProducts()
        {


            var products = await _context.Products.ToListAsync();

            return products;

        }



        [HttpGet("{id}")]


        public async Task <ActionResult<Product>> GetProduct(int id)
        {


            return await _context.Products.FindAsync(id);
        
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
