using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly ITicketTypeRepository _repository;
        public TicketTypeService(ITicketTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<TicketTypeResponse> AddAsync(TicketTypeRequest request)
        {
            return await _repository.AddAsync(request);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<TicketTypeResponse>> GetAllAsync(QueryObject query)
        {
            return await _repository.GetAllAsync(query);
        }

        public async Task<List<TicketTypeResponse>> GetByEventIdAsync(int eventId)
        {
            return await _repository.GetByEventIdAsync(eventId);
        }

        public async Task<TicketTypeResponse> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TicketTypeResponse> UpdateAsync(int id, TicketTypeRequest request)
        {
            return await _repository.UpdateAsync(id, request);
        }
    }
}