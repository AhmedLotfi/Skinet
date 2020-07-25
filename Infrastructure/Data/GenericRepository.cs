using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CoreAudited;
using Core.Interfaces;
using Core.Specifications;
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

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
            => await this.ApplySpecification(spec).ToListAsync();

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
            => await this.ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecification<T> spec)
            => await this.ApplySpecification(spec).CountAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
             => SpecificationEvaluator<T, TPrimaryKey>.GetQuery(_storeContext.Set<T>().AsQueryable(), spec);
    }
}