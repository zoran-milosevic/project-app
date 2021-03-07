using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace ProjectApp.Interface.Repository
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}