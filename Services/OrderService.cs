using ECommerce_App.Models;
using ECommerce_App.Models.OrderAggregate;
using ECommerce_App.Repositories;
using ECommerce_App.Specifications;

namespace ECommerce_App.Services
{
    public class OrderService : IOrderService
    {
      
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitofwork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitofwork)
        {
           
            _basketRepo = basketRepo;
            _unitofwork = unitofwork;
        }

        public async Task<Order> CreateOrder(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket from basket repo
            var basket = await _basketRepo.GetBasketAsync(basketId); 
           
            //get items from the product repo

            var items = new List <OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitofwork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name,
              productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);

            }
            //get delivery method from repo

            var deliveryMethod = await _unitofwork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //check to see if order exists
            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var order = await _unitofwork.Repository<Order>().GetEntityWithSpec(spec);

            if (order != null) //If we already have the order
            {

                order.ShipToAddress = shippingAddress;
                order.DeliveryMethod = deliveryMethod;
                order.Subtotal = subtotal;
                _unitofwork.Repository<Order>().Update(order);


            }
            else 
            
            {



                //create order
                order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId);
                _unitofwork.Repository<Order>().Add(order);



            }

          
            //save to db
            var result = await _unitofwork.Complete();

            if (result <= 0) return null;


           
            //return order

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitofwork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderById(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await _unitofwork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitofwork.Repository<Order>().GetAsync(spec);
        }
    }
}
