using Core.CoreAudited;
using Core.Specifications;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity, TPrimaryKey> where TEntity : AuditedEntity<TPrimaryKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null) query = query.Where(spec.Criteria);

            if (spec.OrderBy != null) query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending != null) query = query.OrderBy(spec.OrderByDescending);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

    }
}