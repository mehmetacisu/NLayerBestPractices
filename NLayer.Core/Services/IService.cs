﻿using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); 
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression); 
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        //IService de dbye yansıtacağımız ve yansıtırken savechangesasync kullanacağımızdan dolayı async yaptık add,update,remove
    }
}
