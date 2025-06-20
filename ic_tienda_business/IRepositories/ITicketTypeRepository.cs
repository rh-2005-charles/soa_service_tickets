using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IRepositories
{
    public interface ITicketTypeRepository
    {
        Task<PaginatedResponse<TicketTypeResponse>> GetAllAsync(QueryObject query);
        Task<TicketTypeResponse> GetByIdAsync(int id);
        Task<TicketTypeResponse> AddAsync(TicketTypeRequest request);
        Task<TicketTypeResponse> UpdateAsync(int id, TicketTypeRequest request);
        Task DeleteAsync(int id);
    }
}