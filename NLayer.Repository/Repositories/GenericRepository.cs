using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System.Linq.Expressions;

namespace NLayer.Repository.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly AppDbContext _context;
		private readonly DbSet<T> _dbSet;
		//readonly olmasının sebebi constructor da yada yukarıda değer atayabiliriz. daha sonra değer atayamayız.
		public GenericRepository(AppDbContext context)
		{
			_context = context;
			_dbSet=_context.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			 await _dbSet.AddAsync(entity);
		}

		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			//belleğe alıyoruz.
			 await _dbSet.AddRangeAsync(entities);
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbSet.AnyAsync(expression);
		}

		public IQueryable<T> GetAll()
		{
			//orderby vs yaparsak diye IQueryable dönüyoruz,AsNoTracking ile memory'e alma diyoruz.
			return _dbSet.AsNoTracking().AsQueryable();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public void Remove(T entity)
		{
			//async methodu yok Remove'un ->remove dediğimiz de EntityState değişti diyoruz db.savechanges dedğimiz de geçerli kılıyor
			_dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return _dbSet.Where(expression);
		}
	}
}
