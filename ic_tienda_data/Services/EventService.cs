using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;
        public EventService(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<EventResponse> AddAsync(EventRequest eventRequest)
        {
            return await _repository.AddAsync(eventRequest);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<EventResponse>> GetAllAsync(QueryObject query)
        {
            return await _repository.GetAllAsync(query);
        }

        public async Task<EventResponse> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<EventResponse> UpdateAsync(int id, EventRequest eventRequest)
        {
            return await _repository.UpdateAsync(id, eventRequest);
        }
    }
}