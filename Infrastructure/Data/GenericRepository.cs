using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CoreAudited;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T, TPrimaryKey> : IGenericRepository<T, TPrimaryKey> where T : AuditedEntity<TPrimaryKey>
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _storeContext.Set<T>().ToListAsync();


        public async Task<T> GetByIdAsync(TPrimaryKey id) => await _storeContext.Set<T>().FindAsync(id);
    }
}