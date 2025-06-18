using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> AddAsync(CategoryRequest categoryRequest)
        {
            return await _categoryRepository.AddAsync(categoryRequest);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<CategoryResponse>> GetAllAsync(QueryObject query)
        {
            return await _categoryRepository.GetAllAsync(query);
        }

       

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, CategoryRequest categoryRequest)
        {
            await _categoryRepository.UpdateAsync(id, categoryRequest);
        }
    }
}