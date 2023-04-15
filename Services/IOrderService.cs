using ECommerce_App.Models.OrderAggregate;

namespace ECommerce_App.Services
{
    public interface IOrderService
    {
       Task<Order> CreateOrder(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);//creates an order

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

        Task<Order> GetOrderById(int id, string buyerEmail);//get order by single id

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
