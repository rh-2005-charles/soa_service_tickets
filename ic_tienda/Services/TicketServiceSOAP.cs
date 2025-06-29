using CoreWCF;
using ic_tienda.Contracts;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;

namespace ic_tienda.Services
{
    [ServiceBehavior(Namespace = "http://tempuri.org/")]
    public class TicketServiceSOAP : ITicketServiceSOAP
    {
        private readonly ITicketService _service;
        public TicketServiceSOAP(ITicketService service)
        {
            _service = service;
        }

        public TicketResponse Add(TicketRequest request)
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

        public TicketPaginatedResponse GetAll(QueryObject query)
        {
            var result = _service.GetAllAsync(query).GetAwaiter().GetResult();

            return new TicketPaginatedResponse
            {
                Items = result.Items,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };
        }

        public List<TicketResponse> GetById(int id)
        {
            try
            {
                return _service.GetByIdAsync(id).GetAwaiter().GetResult();
            }
            catch
            {
                throw new FaultException($"Item con ID {id} no encontrado.");
            }
        }

        public List<TicketResponse> GetByOrderDetailId(int id)
        {
            try
            {
                return _service.GetByOrderDetailIdAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al obtener detalles para la orden {id}: {ex.Message}");
            }
        }

        public TicketResponse Update(int id, TicketRequest request)
        {
            try
            {
                return _service.UpdateAsync(id, request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al actualizar: {ex.Message}");
            }
        }
    }
}