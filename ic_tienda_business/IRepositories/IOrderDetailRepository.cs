using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IRepositories
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetailResponse> GetByIdAsync(int id);
        Task<OrderDetailResponse> AddAsync(OrderDetailRequest orderDetailRequest);

        Task<IEnumerable<OrderDetailResponse>> GetByOrderIdDetailAsync(int orderId);
    }
}