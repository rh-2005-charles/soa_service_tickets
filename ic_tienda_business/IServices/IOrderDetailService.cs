using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IServices
{
    public interface IOrderDetailService
    {
        Task<PaginatedResponse<OrderDetailResponse>> GetAllAsync(QueryObject query);
        Task<OrderDetailResponse> GetByIdAsync(int id);
        Task<OrderDetailResponse> AddAsync(OrderDetailRequest request);
        Task<OrderDetailResponse> UpdateAsync(int id, OrderDetailRequest request);
        Task DeleteAsync(int id);

        Task<List<OrderDetailResponse>> GetByOrderIdAsync(int orderId);

    }
}