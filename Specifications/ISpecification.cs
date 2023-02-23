using System.Linq.Expressions;

namespace ECommerce_App.Specifications
{
    public interface ISpecification<T>
    {

        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }
    }
}

