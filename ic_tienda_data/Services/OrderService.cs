using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderResponse> AddAsync(OrderRequest request)
        {
            return await _repository.AddAsync(request);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<OrderResponse>> GetAllAsync(QueryObject query)
        {
            return await _repository.GetAllAsync(query);
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<OrderResponse> UpdateAsync(int id, OrderRequest request)
        {
            return await _repository.UpdateAsync(id, request);
        }
    }
}