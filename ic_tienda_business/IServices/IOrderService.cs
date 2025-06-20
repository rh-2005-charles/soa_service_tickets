using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IServices
{
    public interface IOrderService
    {
        Task<PaginatedResponse<OrderResponse>> GetAllAsync(QueryObject query);
        Task<OrderResponse> GetByIdAsync(int id);
        Task<OrderResponse> AddAsync(OrderRequest request);
        Task<OrderResponse> UpdateAsync(int id, OrderRequest request);
        Task DeleteAsync(int id);
    }
}