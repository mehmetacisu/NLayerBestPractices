using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        //productRepository.GetAll(x=>x.id>5).toList(); // IQueryable memory de çalışır
        //ToList , ToListAsync metotları çalışınca dbye gider.
        IQueryable<T> GetAll(); 


        //IQueryable direkt veritabanına gitmez. 
        // productRepository.where(x=>x.id).OrderBy().ToListAsync(); to list methoduna kadar dbye gitmeden sorgu yapar 
        //IQueryable kullanmazsak önce where sorgusunu db de çalıştırır sonra memorye alır ardından orderby yapar.

        IQueryable<T> Where(Expression<Func<T, bool>> expression); //20 satırdan 10u true dönerse 10 kayıt döner.
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);  // var mı yok mu onu dönecek

        Task AddRangeAsync(IEnumerable<T> entities);
        Task AddAsync(T entity);

        //update veya remove uzun süren bir işlem olmadığı için async metotları yoktur. 
        //state değiştiriyor sadece.
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
