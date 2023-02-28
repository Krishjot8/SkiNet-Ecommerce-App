using ECommerce_App.Data;
using ECommerce_App.Models;
using ECommerce_App.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_App.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public  async Task<T> GetByIdAsync(int id)
        {
             return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAsync(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec) 
        
        {

            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(),spec);
        
        
        }

       
    }
}
