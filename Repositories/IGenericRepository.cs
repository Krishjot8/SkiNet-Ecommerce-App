

using ECommerce_App.Models;
using ECommerce_App.Specifications;

namespace ECommerce_App.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel

    {


        Task<T> GetByIdAsync(int id);   

        Task<List<T>> GetAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        Task<List<T>>GetAsync (ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);
    }
}
