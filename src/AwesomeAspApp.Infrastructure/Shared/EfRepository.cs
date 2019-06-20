using AwesomeAspApp.Core.Interfaces.Gateways.Repositories;
using AwesomeAspApp.Core.Shared;
using AwesomeAspApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeAspApp.Infrastructure.Shared
{
    public abstract class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _appDbContext;

        protected EfRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual Task<T> GetById(int id)
        {
            return _appDbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ListAll()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public Task<T> GetSingleBySpec(ISpecification<T> spec)
        {
            return ListInternal(spec).FirstOrDefaultAsync();
        }

        public Task<List<T>> List(ISpecification<T> spec)
        {
            return ListInternal(spec).ToListAsync();
        }

        private IQueryable<T> ListInternal(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appDbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult.Where(spec.Criteria);
        }


        public async Task<T> Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public Task Update(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return _appDbContext.SaveChangesAsync();
        }

        public Task Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            return _appDbContext.SaveChangesAsync();
        }
    }
}
