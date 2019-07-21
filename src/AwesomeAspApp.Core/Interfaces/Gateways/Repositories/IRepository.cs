
using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAspApp.Core.Shared;

namespace AwesomeAspApp.Core.Interfaces.Gateways.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetById(int id);
        Task<IList<T>> GetAll();
        Task<T?> FirstOrDefaultBySpecs(params ISpecification<T>[] specs);
        Task<IList<T>> GetAllBySpecs(params ISpecification<T>[] spec);

        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}