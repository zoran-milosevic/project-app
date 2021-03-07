using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectApp.Data.Context;
using ProjectApp.Common;
using ProjectApp.Interface.Repository;

namespace ProjectApp.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ILogger<GenericRepository<T>> _logger;
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ILogger<GenericRepository<T>> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IEnumerable<T> result = null;

            await Task.Run(() =>
            {
                result = GetIncluding(includeProperties).ToList();
            });

            return result;
        }

        public async Task<T> GetById(int id)
        {
            Expression<Func<T, bool>> lambda = Utilities<T>.BuildLambdaForGetById(id);

            return await _dbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(lambda);
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> result = await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<T>> FindAllIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> result = null;

            await Task.Run(() =>
            {
                result = GetIncluding(includeProperties);
            });

            return result.Where(predicate).ToList();
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            await Task.Run(() =>
            {
                _dbSet.Update(entity);
            });
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            _dbSet.Remove(entity);
        }

        private IQueryable<T> GetIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = _dbSet.AsNoTracking();

            return includeProperties.Aggregate(queryable, (current, includeProperties) => current.Include(includeProperties));
        }
    }
}
