using AutoMapper;
using SanStore.Application.DTO.BrandDtos;
using SanStore.Application.DTO.CategoryDtos;
using SanStore.Application.DTO.ProductDto;
using SanStore.Application.InputModels;
using SanStore.Application.Services.Interface;
using SanStore.Application.ViewModels;
using SanStore.Domain.Contracts;
using SanStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRespositoty _productRepository;
        private readonly IPaginationService<ProductDto,Product> _paginationService;
        private readonly IMapper _mapper;

        public ProductService(IProductRespositoty productRepository, IMapper mapper, IPaginationService<ProductDto, Product> paginationService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var createdEntity = await _productRepository.CreateAsync(product);
            var entity = _mapper.Map<ProductDto>(createdEntity);
            return entity;
        }


        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(x=>x.Id == id);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllProductAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetAllbyFilterAsync(int? categoryId, int? brandId)
        {
            var data = await _productRepository.GetAllProductAsync();
            IEnumerable<Product> query = data;

            if(categoryId > 0)
            {
                query = query.Where(x=>x.categoryId == categoryId);
            }

            if(brandId > 0)
            {
                query = query.Where(x => x.BrandId == brandId);
            }
            var result = _mapper.Map<List<ProductDto>>(query);
            return result;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetDetailAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PaginationVM<ProductDto>> GetPagination(PaginationInputModel pagination)
        {
            var source = await _productRepository.GetAllProductAsync();

            var result = _paginationService.GetPagination(source, pagination);
            return result;
        }

        public async Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            await _productRepository.UpdateAsync(product);
        }
    }
}
