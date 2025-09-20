using SanStore.Application.DTO;
using SanStore.Application.Services.Interface;
using SanStore.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<CategoryDto> Create(CreateCategoryDTO createCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(UpdateCategoryDto updateCategoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
