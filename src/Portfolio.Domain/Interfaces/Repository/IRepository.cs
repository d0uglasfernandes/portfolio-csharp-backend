using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<T> SelectAsync(Guid id);
        Task<IEnumerable<T>> SelectAsync();
        Task<bool> IsExists(Guid id);
        IQueryable<T> ListNoTracking(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultNoTrackingAsync(Expression<Func<T, bool>> predicate);
        DbSet<T> DataSet { get; }
        Task<bool> InsertListAsync(List<T> listEntity);
        Task<bool> UpdateListAsync(List<T> listEntity);
        
    }
}