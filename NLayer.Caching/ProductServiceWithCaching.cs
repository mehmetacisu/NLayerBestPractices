using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Caching
{
	public class ProductServiceWithCaching : IProductService
	{
		private const string CacheProductKey = "productsCache";
		private readonly IMapper _mapper;
		private readonly IMemoryCache _cache;
		private readonly ProductRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public ProductServiceWithCaching(IMapper mapper, IMemoryCache cache, ProductRepository repository, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_cache = cache;
			_repository = repository;
			_unitOfWork = unitOfWork;

			if (!_cache.TryGetValue(CacheProductKey, out _)) //memory allocate etmesini önler _
			{
				_cache.Set(CacheProductKey, _repository.GetProductsWithCategory());
			}
		}

		public async Task<Product> AddAsync(Product entity)
		{
			await _repository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			await CacheAllProductsAsync();
			return entity;
		}

		public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
		{
			await _repository.AddRangeAsync(entities);
			await _unitOfWork.CommitAsync();
			await CacheAllProductsAsync();
			return entities;
		}

		public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Product>> GetAllAsync()
		{
			return Task.FromResult(_cache.Get<IEnumerable<Product>>(CacheProductKey));
		}

		public Task<Product> GetByIdAsync(int id)
		{
			var product = _cache.Get<List<Product>>(CacheProductKey)
						  .FirstOrDefault(x => x.Id == id);
			if (product is null)
			{
				throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
			}
			return Task.FromResult(product);
		}

		public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
		{
			var products = _cache.Get<IEnumerable<Product>>(CacheProductKey);

			var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

			return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto));
		}

		public async Task RemoveAsync(Product entity)
		{
			_repository.Remove(entity);
			await _unitOfWork.CommitAsync();
			await CacheAllProductsAsync();
		}

		public async Task RemoveRangeAsync(IEnumerable<Product> entities)
		{
			_repository.RemoveRange(entities);
			await _unitOfWork.CommitAsync();
			await CacheAllProductsAsync();
		}

		public async Task UpdateAsync(Product entity)
		{
			_repository.Update(entity);
			await _unitOfWork.CommitAsync();
			await CacheAllProductsAsync();
		}

		public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
		{
			return _cache.Get<List<Product>>(CacheProductKey)
				.Where(expression.Compile())
				.AsQueryable();
		}

		public async Task CacheAllProductsAsync()
		{
			await _cache.Set(CacheProductKey, _repository.GetAll().ToListAsync());
		}
	}
}
