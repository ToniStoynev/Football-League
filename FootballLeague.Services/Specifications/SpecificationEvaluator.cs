using FootballLeague.Domain;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Services.Specifications;

internal class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQueryable,
        Specification<TEntity> specification)
        where TEntity : BaseEntity<Guid>
    {
        IQueryable<TEntity> queryalbe = inputQueryable;

        if (specification is null)
        {
            return queryalbe;
        }

        if (specification.Criteria is not null)
        {
            queryalbe = queryalbe.Where(specification.Criteria);
        }

        queryalbe = specification.IncludeExpressions.Aggregate(queryalbe,
            (current, includeExpression) => current.Include(includeExpression));

        if (specification.OrderByExpression is not null)
        {
            queryalbe = queryalbe.OrderBy(specification.OrderByExpression);
        }
        else if(specification.OrderByDescendingExpression is not null)
        {
            queryalbe = queryalbe.OrderByDescending(specification.OrderByDescendingExpression);
        }

        return queryalbe;
    }
}
