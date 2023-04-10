using ECommerce_App.Models;

namespace ECommerce_App.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);

        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

        Task <bool> DeleteBasketAsync(string basketId);

    }
}
