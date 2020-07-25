using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CoreAudited;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T, TPrimaryKey> where T : AuditedEntity<TPrimaryKey>
    {

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);

        Task<T> GetByIdAsync(TPrimaryKey id);

        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);
    }
}