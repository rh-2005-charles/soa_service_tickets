using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketTypeRepository _ticketTypeRepository;
        public EventService(IEventRepository repository,
            ITicketRepository ticketRepository,
            IOrderRepository orderRepository,
            ITicketTypeRepository ticketTypeRepository
        )
        {
            _eventRepository = repository;
            _ticketTypeRepository = ticketTypeRepository;
            _ticketRepository = ticketRepository;
            _orderRepository = orderRepository;
        }

        public async Task<EventResponse> AddAsync(EventRequest eventRequest)
        {
            return await _eventRepository.AddAsync(eventRequest);
        }

        public async Task CancelEventAsync(int eventId)
        {
            // 1. Actualizar el estado del evento
            var eventToCancel = await _eventRepository.GetByIdAsync(eventId);
            if (eventToCancel == null)
                throw new KeyNotFoundException("Evento no encontrado");

            var updateRequest = new EventRequest
            {
                Name = eventToCancel.Name,
                Description = eventToCancel.Description,
                Date = eventToCancel.Date,
                Location = eventToCancel.Location,
                // ImgPath = eventToCancel.ImageUrl,
                Status = "Cancelado" // Nuevo estado
            };
            await _eventRepository.UpdateAsync(eventId, updateRequest);

            // 2. Actualizar todos los tickets asociados al evento
            var tickets = await _ticketRepository.GetByEventIdAsync(eventId);
            foreach (var ticket in tickets)
            {
                var ticketUpdate = new TicketRequest
                {
                    OrderDetailId = ticket.OrderDetailId,
                    EventId = ticket.EventId,
                    TicketTypeId = ticket.TicketTypeId,
                    CustomerId = ticket.CustomerId,
                    Status = "Cancelado", // Nuevo estado
                    SeatNumber = ticket.SeatNumber,
                    TicketUrl = ticket.TicketUrl,
                    QrCode = ticket.QrCode
                };
                await _ticketRepository.UpdateAsync(ticket.Id, ticketUpdate);
            }

            // 3. Actualizar órdenes relacionadas (opcional)
            var orders = await _orderRepository.GetByEventIdAsync(eventId);
            foreach (var order in orders)
            {
                var orderUpdate = new OrderRequest
                {
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = "Evento Cancelado", // Nuevo estado
                    PaymentMethod = order.PaymentMethod,
                    TransactionId = order.TransactionId
                };
                await _orderRepository.UpdateAsync(order.Id, orderUpdate);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _eventRepository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<EventResponse>> GetAllAsync(QueryObject query)
        {
            return await _eventRepository.GetAllAsync(query);
        }

        public async Task<EventResponse> GetByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task<EventResponse> UpdateAsync(int id, EventRequest eventRequest)
        {
            return await _eventRepository.UpdateAsync(id, eventRequest);
        }
    }
}