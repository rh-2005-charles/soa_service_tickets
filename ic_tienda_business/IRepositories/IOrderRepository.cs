using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IRepositories
{
    public interface IOrderRepository
    {
        Task<OrderResponse> GetByIdAsync(int id);
        Task<OrderResponse> AddAsync(OrderRequest orderRequest);
        Task<OrderResponse> UpdateAsync(int id, OrderRequest orderRequest);
       // Task<PaginatedResponse<OrderResponse>> GetAllAsync(QueryObjectOrder query);
       // Task<PaginatedResponse<OrderResponse>> GetAllInProcessAsync(QueryObjectOrder query);
        Task DeleteAsync(OrderResponse orderResponse);
        Task<IEnumerable<OrderDetailResponse>> GetOrderDetailsAsync(int orderId);//GetInProcessOrdersByCustomerIdAsync
        Task<IEnumerable<OrderResponse>> GetInProcessOrdersByCustomerIdAsync(int customerId);
      //  Task<PaginatedResponse<OrderResponse>> GetOrdersByStatusAsync(QueryObjectOrderTwo query);
    }
}