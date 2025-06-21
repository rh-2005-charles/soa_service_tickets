using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IServices
{
    public interface IEventService
    {
        Task<PaginatedResponse<EventResponse>> GetAllAsync(QueryObject query);
        Task<EventResponse> GetByIdAsync(int id);
        Task<EventResponse> AddAsync(EventRequest eventRequest);
        Task<EventResponse> UpdateAsync(int id, EventRequest eventRequest);
        Task DeleteAsync(int id);

        Task CancelEventAsync(int eventId);
    }
}