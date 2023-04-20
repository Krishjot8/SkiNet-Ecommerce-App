using ECommerce_App.Models;
using ECommerce_App.Models.OrderAggregate;

namespace ECommerce_App.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);

        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
        
        
    }
}
