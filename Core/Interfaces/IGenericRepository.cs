using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CoreAudited;

namespace Core.Interfaces
{
    public interface IGenericRepository<T, TPrimaryKey> where T : AuditedEntity<TPrimaryKey>
    {
        Task<T> GetByIdAsync(TPrimaryKey id);

        Task<IReadOnlyList<T>> GetAllAsync();
    }
}