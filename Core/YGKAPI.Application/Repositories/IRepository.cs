using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Domain.Entities;

namespace YGKAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll(bool tracking = true, Func<IQueryable<T>, IQueryable<T>> include = null);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<T> GetByIdAsync(string id, bool tracking = true, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T model);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<T> datas);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
