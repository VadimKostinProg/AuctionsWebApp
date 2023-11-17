using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.RepositoryContracts;
using BidMasterOnline.Domain.Entities;
using BidMasterOnline.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BidMasterOnline.Infrastructure.Repositories
{
    public class RepositoryBase : IAsyncRepository
    {
        private readonly ApplicationContext _context;

        public RepositoryBase(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T entity) where T : EntityBase
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : EntityBase
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<bool> DeleteAsync<T>(Guid id) where T : EntityBase
        {
            var entityToDelete = _context.Set<T>().Find(id);

            if (entityToDelete is null)
            {
                return false;
            }

            _context.Set<T>().Remove(entityToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<int> DeleteManyAsync<T>(IEnumerable<T> entities) where T : EntityBase
        {
            throw new NotImplementedException();
        }

        public async Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression, bool disableTracking = true) where T : EntityBase
        {
            var query = _context.Set<T>().AsQueryable();

            if (!disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(bool disableTracking = true) where T : EntityBase
        {
            var query = _context.Set<T>().AsQueryable();

            if (!disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(Specifications specifications, bool disableTracking = true) where T : EntityBase
        {
            var query = _context.Set<T>().AsQueryable();

            if (specifications is not null)
            {
                query = ApplySorting(query, specifications);
                query = ApplyPaginating(query, specifications);
            }

            if (!disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync<T>(Guid id, bool disableTracking = true) where T : EntityBase
        {
            var query = _context.Set<T>().AsQueryable();

            if (!disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetFilteredAsync<T>(Expression<Func<T, bool>> predicate, bool disableTracking = true) where T : EntityBase
        {
            var query = _context.Set<T>().Where(predicate);

            if (!disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetFilteredAsync<T>(Expression<Func<T, bool>> predicate, Specifications specifications, bool disableTracking = true) where T : EntityBase
        {
            var query = _context.Set<T>().Where(predicate);

            if (specifications is not null)
            {
                query = ApplySorting(query, specifications);
                query = ApplyPaginating(query, specifications);
            }

            if (!disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public Task UpdateAsync<T>(T entity) where T : EntityBase
        {
            throw new NotImplementedException();
        }

        private IQueryable<T> ApplySorting<T>(IQueryable<T> entities, Specifications specifications)
        {
            throw new NotImplementedException();
        }

        private IQueryable<T> ApplyPaginating<T>(IQueryable<T> entities, Specifications specifications)
        {
            throw new NotImplementedException();
        }
    }
}
