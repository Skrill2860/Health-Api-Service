using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Health.Service.Entities;

namespace Health.Service.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T item);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(T item);
    }
}