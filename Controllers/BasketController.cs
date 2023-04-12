
using AutoMapper;
using ECommerce_App.DTOs;
using ECommerce_App.Models;
using ECommerce_App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_App.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet]

        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {

            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id)); //return basket but if no basket return new customer basket and use id the client has generated 
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        
        {

            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
        
            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            return Ok(updatedBasket);
        }

        [HttpDelete]

        public async Task DeleteBasket(string id)
        { 
        
        await _basketRepository.DeleteBasketAsync(id);
        
        }
    }
}
