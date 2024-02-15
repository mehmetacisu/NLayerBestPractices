using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ProductController : CustomBaseController
	{
		private readonly IMapper _mapper;
		//private readonly IService<Product> _productService;
		private readonly IProductService _productService;
		public ProductController(IMapper mapper, IProductService productService)
		{
			_mapper = mapper;
			_productService = productService;
		}


		//GET api/products/GetProductsWithCategory
		//[HttpGet("GetProductsWithCategory")]
		[HttpGet("[action]")] //action ismi alır
		public async Task<IActionResult> GetProductsWithCategory()
		{
			return CreateActionResult(await _productService.GetProductsWithCategory());
		}

		//GET api/product
		[HttpGet]
		public async Task<IActionResult> All()
		{
			var products = await _productService.GetAllAsync();
			var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());
			//return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
			return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
		}

		//GET api/product/5
		//süslü içinde belirtmezsek QueryString üzerinde bekler 
		[ServiceFilter(typeof(NotFoundFilter<Product>))]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			var productsDto = _mapper.Map<ProductDto>(product);
			//return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
			return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
		}
		[HttpPost]
		public async Task<IActionResult> Add(ProductDto productDto)
		{
			var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));
			var productsDto = _mapper.Map<ProductDto>(product);
			//return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
			return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
		}
		[HttpPut]
		public async Task<IActionResult> Update(ProductUpdateDto productDto)
		{
			await _productService.UpdateAsync(_mapper.Map<Product>(productDto));
			//return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
			return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
		}

		//DELETE api/products/5 
		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			if (product == null)
			{
				return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Bu id'ye sahip ürün bulunamadı"));
			}
			await _productService.RemoveAsync(product);
			//return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
			return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
		}
	}
}
