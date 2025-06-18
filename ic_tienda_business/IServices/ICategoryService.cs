using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IServices
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetByIdAsync(int id);
        Task<CategoryResponse> AddAsync(CategoryRequest categoryRequest);
        Task UpdateAsync(int id, CategoryRequest categoryRequest);
        Task DeleteAsync(int id);
        Task<PaginatedResponse<CategoryResponse>> GetAllAsync(QueryObject query);

    }
}