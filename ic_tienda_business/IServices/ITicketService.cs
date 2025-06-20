using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IServices
{
    public interface ITicketService
    {
        Task<PaginatedResponse<TicketResponse>> GetAllAsync(QueryObject query);
        Task<TicketResponse> GetByIdAsync(int id);
        Task<TicketResponse> AddAsync(TicketRequest request);
        Task<TicketResponse> UpdateAsync(int id, TicketRequest request);
        Task DeleteAsync(int id);
    }
}