using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Repositories;
using YGKAPI.Domain.Entities;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly YGKAPIMySQLDbContext _context;
        public Repository(YGKAPIMySQLDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var query = Table.AsQueryable();
            if (!tracking) { query = query.AsNoTracking(); }

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var query = Table.Where(method);
            if (!tracking) { query = query.AsNoTracking(); }

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var query = Table.AsQueryable();
            if (!tracking) { query = query.AsNoTracking(); }

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(method);
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var query = Table.AsQueryable();
            if (!tracking) { query = query.AsNoTracking(); }
            
            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }
        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }
        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }
        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            return Remove(model);
        }
        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }
        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}