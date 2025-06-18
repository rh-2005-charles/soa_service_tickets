using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IRepositories
{
    public interface ICategoryRepository
    {
        Task<CategoryResponse> GetByIdAsync(int id);
        Task<CategoryResponse> AddAsync(CategoryRequest categoryRequest);
        Task<CategoryResponse> UpdateAsync(int id, CategoryRequest categoryRequest);
        Task DeleteAsync(int id);
        //ACTUALIZADO

        Task<PaginatedResponse<CategoryResponse>> GetAllAsync(QueryObject query);
    }
}