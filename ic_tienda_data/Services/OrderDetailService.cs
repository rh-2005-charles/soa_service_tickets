using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _repository;
        public OrderDetailService(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderDetailResponse> AddAsync(OrderDetailRequest request)
        {
            return await _repository.AddAsync(request);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<OrderDetailResponse>> GetAllAsync(QueryObject query)
        {
            return await _repository.GetAllAsync(query);
        }

        public async Task<OrderDetailResponse> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<OrderDetailResponse>> GetByOrderIdAsync(int orderId)
        {
            return await _repository.GetByOrderIdAsync(orderId);
        }

        public async Task<OrderDetailResponse> UpdateAsync(int id, OrderDetailRequest request)
        {
            return await _repository.UpdateAsync(id, request);
        }
    }
}