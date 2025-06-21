using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IRepositories
{
    public interface IEventRepository
    {
        Task<PaginatedResponse<EventResponse>> GetAllAsync(QueryObject query);
        Task<EventResponse> GetByIdAsync(int id);
        Task<EventResponse> AddAsync(EventRequest eventRequest);
        Task<EventResponse> UpdateAsync(int id, EventRequest eventRequest);
        Task DeleteAsync(int id);

    }
}