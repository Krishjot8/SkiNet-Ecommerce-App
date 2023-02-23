using ECommerce_App.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_App.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseModel
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity>
            spec)
        
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            { 
            
            query= query.Where(spec.Criteria);
            
            }


            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;

        }

    }
}
