using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda_business.IRepositories
{
    public interface ITicketRepository
    {
        Task<PaginatedResponse<TicketResponse>> GetAllAsync(QueryObject query);
        // Task<TicketResponse> GetByIdAsync(int id);
        Task<List<TicketResponse>> GetByIdAsync(int id);
        Task<TicketResponse> AddAsync(TicketRequest request);
        Task<TicketResponse> UpdateAsync(int id, TicketRequest request);
        Task DeleteAsync(int id);

        Task<List<TicketResponse>> GetByOrderDetailIdAsync(int orderDetailId);

        Task<List<TicketResponse>> GetByEventIdAsync(int eventId);

        // Auto Incremental
        Task<int> GetNextSeatNumberAsync(int eventId);
        Task<int> GetNextTicketNumberAsync();
    }
}