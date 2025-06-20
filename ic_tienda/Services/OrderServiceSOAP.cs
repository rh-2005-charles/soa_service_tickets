using CoreWCF;
using ic_tienda.Contracts;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;

namespace ic_tienda.Services
{
    [ServiceBehavior(Namespace = "http://tempuri.org/")]
    public class OrderServiceSOAP : IOrderServiceSOAP
    {
        private readonly IOrderService _service;
        public OrderServiceSOAP(IOrderService service)
        {
            _service = service;
        }

        public OrderResponse Add(OrderRequest request)
        {
            try
            {
                return _service.AddAsync(request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al crear el ticket: {ex.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _service.DeleteAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al eliminar el ticket: {ex.Message}");
            }
        }

        public OrderPaginatedResponse GetAll(QueryObject query)
        {
            var result = _service.GetAllAsync(query).GetAwaiter().GetResult();

            return new OrderPaginatedResponse
            {
                Items = result.Items,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };
        }

        public OrderResponse GetById(int id)
        {
            try
            {
                return _service.GetByIdAsync(id).GetAwaiter().GetResult();
            }
            catch
            {
                throw new FaultException($"Evento con ID {id} no encontrado.");
            }
        }

        public OrderResponse Update(int id, OrderRequest request)
        {
            try
            {
                return _service.UpdateAsync(id, request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al actualizar el evento: {ex.Message}");
            }
        }
    }
}