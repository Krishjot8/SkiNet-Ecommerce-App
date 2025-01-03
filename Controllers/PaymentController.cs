﻿using ECommerce_App.Errors;
using ECommerce_App.Models;
using ECommerce_App.Models.OrderAggregate;
using ECommerce_App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ECommerce_App.Controllers
{

    public class PaymentController : BaseApiController
    {

        private const string WhSecret = "whsec_6b5bae0ed7f0fdd186c5b69bf246904d62e7b5ed4ebe8444c9836398e69d408f";

        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }





       [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
         
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return basket;
        }




        [HttpPost("webhook")]

        public async Task<ActionResult> StripeWebhook() 
        
        {

            var json = await new StreamReader(Request.Body).ReadToEndAsync(); //gives body of request as json formatted contents

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;

            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment succeeded:", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: ", order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed:", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Order updated to payment failed: ", order.Id);
                    break;
                
            
            }
        

            return new EmptyResult();
        
        }



    }

}