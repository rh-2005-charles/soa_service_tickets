using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;
        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<TicketResponse> AddAsync(TicketRequest request)
        {
            return await _repository.AddAsync(request);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<TicketResponse>> GetAllAsync(QueryObject query)
        {
            return await _repository.GetAllAsync(query);
        }

        public async Task<List<TicketResponse>> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<TicketResponse>> GetByOrderDetailIdAsync(int orderDetailId)
        {
            return await _repository.GetByOrderDetailIdAsync(orderDetailId);
        }

        public async Task<TicketResponse> UpdateAsync(int id, TicketRequest request)
        {
            return await _repository.UpdateAsync(id, request);
        }
    }
}