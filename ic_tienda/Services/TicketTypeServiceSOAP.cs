using CoreWCF;
using ic_tienda.Contracts;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;

namespace ic_tienda.Services
{
    [ServiceBehavior(
       Namespace = "http://tempuri.org/",
       InstanceContextMode = InstanceContextMode.PerCall,
       IncludeExceptionDetailInFaults = true
   )]
    public class TicketTypeServiceSOAP : ITicketTypeServiceSOAP
    {
        private readonly ITicketTypeService _service;
        public TicketTypeServiceSOAP(ITicketTypeService service)
        {
            _service = service;
        }

        public TicketTypeResponse Add(TicketTypeRequest request)
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

        public TicketTypePaginatedResponse GetAll(QueryObject query)
        {
            var result = _service.GetAllAsync(query).GetAwaiter().GetResult();

            return new TicketTypePaginatedResponse
            {
                Events = result.Items,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };
        }

        public TicketTypeResponse GetById(int id)
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

        public TicketTypeResponse Update(int id, TicketTypeRequest request)
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