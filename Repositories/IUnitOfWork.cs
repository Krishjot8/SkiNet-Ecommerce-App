using ECommerce_App.Models;

namespace ECommerce_App.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;

        Task<int> Complete();
    }
}
