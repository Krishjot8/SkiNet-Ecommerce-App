﻿using AutoMapper;
using ECommerce_App.Data;
using ECommerce_App.DTOs;
using ECommerce_App.Errors;
using ECommerce_App.Helpers;
using ECommerce_App.Models;
using ECommerce_App.Repositories;
using ECommerce_App.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_App.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsrepository;
        private readonly IGenericRepository<ProductBrand> _productbrandrepository;
        private readonly IGenericRepository<ProductType> _producttyperepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsrepository,
          IGenericRepository<ProductBrand> productbrandrepository,
          IGenericRepository<ProductType> producttyperepository, IMapper mapper)
        {
            _productsrepository = productsrepository;
            _productbrandrepository = productbrandrepository;
            _producttyperepository = producttyperepository;
            _mapper = mapper;
        }


        [Cached(600)]
        [HttpGet]

        public async Task<ActionResult <Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productparams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productparams);

            var countSpec = new ProductWithFiltersForCountSpecification(productparams);

            var totalItems = await _productsrepository.CountAsync(countSpec);

            var products = await _productsrepository.GetAsync(spec);

            var data = _mapper.Map<List<Product>, List<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productparams.PageIndex,productparams.PageSize, totalItems, data));
        }



        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]


        public async Task <ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsrepository.GetEntityWithSpec(spec);

            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [Cached(600)]
        [HttpGet("brands")]

        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {


            var productbrands = await _productbrandrepository.GetAllAsync();

            return Ok(productbrands);

        }


        [Cached(600)]
        [HttpGet("brands/{id}")]


        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {


            return await _productbrandrepository.GetByIdAsync(id);

        }

        [Cached(600)]
        [HttpGet("types")]

        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {


            var producttypes = await _producttyperepository.GetAllAsync();

            return Ok(producttypes);

        }

        [Cached(600)]
        [HttpGet("types/{id}")]


        public async Task<ActionResult<ProductType>> GetProductType(int id)
        {


            return await _producttyperepository.GetByIdAsync(id);

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
