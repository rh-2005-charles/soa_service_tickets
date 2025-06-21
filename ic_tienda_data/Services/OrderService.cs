using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderService(
        IOrderRepository orderRepository,
        ITicketTypeRepository ticketTypeRepository,
        IOrderDetailRepository orderDetailRepository,
        ITicketRepository ticketRepository,
        IEventRepository eventRepository,
        ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _ticketTypeRepository = ticketTypeRepository;
            _orderDetailRepository = orderDetailRepository;
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderResponse> AddAsync(OrderRequest request)
        {
            // 1. Validar que el cliente existe
            var customer = await _customerRepository.GetById(request.CustomerId);
            if (customer == null) throw new KeyNotFoundException("Cliente no encontrado");

            // Validar y calcular total
            decimal totalAmount = 0;
            foreach (var item in request.Items)
            {
                var ticketType = await _ticketTypeRepository.GetByIdAsync(item.TicketTypeId);
                if (ticketType == null)
                    throw new KeyNotFoundException($"Tipo de ticket {item.TicketTypeId} no encontrado");

                if (ticketType.Quantity < item.Quantity)
                    throw new InvalidOperationException($"No hay suficientes tickets disponibles para {ticketType.Name}");

                totalAmount += ticketType.Price * item.Quantity;
            }

            // Crear la orden principal
            var orderRequest = new OrderRequest
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = "Pending",
                PaymentMethod = request.PaymentMethod,
                TransactionId = request.TransactionId
            };

            var orderResponse = await _orderRepository.AddAsync(orderRequest);

            // Crear OrderDetails y Tickets
            foreach (var item in request.Items)
            {
                var ticketType = await _ticketTypeRepository.GetByIdAsync(item.TicketTypeId);

                // Crear OrderDetail
                var orderDetailRequest = new OrderDetailRequest
                {
                    OrderId = orderResponse.Id,
                    TicketTypeId = item.TicketTypeId,
                    Quantity = item.Quantity,
                    SubTotal = ticketType.Price * item.Quantity
                };
                var orderDetailResponse = await _orderDetailRepository.AddAsync(orderDetailRequest);

                // Actualizar disponibilidad
                var updateTicketTypeRequest = new TicketTypeRequest
                {
                    Name = ticketType.Name,
                    Price = ticketType.Price,
                    Quantity = ticketType.Quantity - item.Quantity,
                    Description = ticketType.Description,
                    EventId = ticketType.EventId
                };
                await _ticketTypeRepository.UpdateAsync(ticketType.Id, updateTicketTypeRequest);

                Console.WriteLine($"Creando ticket - OrderDetailId: {orderDetailResponse.Id}, CustomerId: {request.CustomerId}");

                // Generar Tickets
                for (int i = 0; i < item.Quantity; i++)
                {
                    var ticketRequest = new TicketRequest
                    {
                        OrderDetailId = orderDetailResponse.Id,
                        EventId = ticketType.EventId,
                        TicketTypeId = item.TicketTypeId,
                        CustomerId = request.CustomerId,
                        Status = "Active"
                    };

                    // Verificar valores antes de crear el ticket
                    if (ticketRequest.OrderDetailId == 0 || ticketRequest.CustomerId == 0)
                    {
                        throw new Exception("Valores inválidos al crear ticket");
                    }

                    await _ticketRepository.AddAsync(ticketRequest);

                }
            }

            return await _orderRepository.GetByIdAsync(orderResponse.Id);
        }


        public async Task DeleteAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<OrderResponse>> GetAllAsync(QueryObject query)
        {
            return await _orderRepository.GetAllAsync(query);
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<OrderResponse> UpdateAsync(int id, OrderRequest request)
        {
            return await _orderRepository.UpdateAsync(id, request);
        }
    }
}