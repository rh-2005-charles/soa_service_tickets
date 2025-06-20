using CoreWCF;
using ic_tienda.Contracts;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;

namespace ic_tienda.Services
{
    /* [ServiceBehavior(
        Namespace = "http://tempuri.org/",
        InstanceContextMode = InstanceContextMode.PerCall,
        IncludeExceptionDetailInFaults = true
    )] */

    [ServiceBehavior(Namespace = "http://tempuri.org/")]
    public class EventServiceSOAP : IEventServiceSOAP
    {
        private readonly IEventService _eventService;

        public EventServiceSOAP(IEventService eventService)
        {
            _eventService = eventService;
        }

        public EventResponse Add(EventRequest request)
        {
            try
            {
                return _eventService.AddAsync(request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al crear el evento: {ex.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _eventService.DeleteAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al eliminar el evento: {ex.Message}");
            }
        }

        public EventPaginatedResponse GetAll(QueryObject query)
        {
            var result = _eventService.GetAllAsync(query).GetAwaiter().GetResult();

            return new EventPaginatedResponse
            {
                Items = result.Items,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };
        }

        public EventResponse GetById(int id)
        {
            try
            {
                return _eventService.GetByIdAsync(id).GetAwaiter().GetResult();
            }
            catch
            {
                throw new FaultException($"Evento con ID {id} no encontrado.");
            }
        }

        public EventResponse Update(int id, EventRequest request)
        {
            try
            {
                return _eventService.UpdateAsync(id, request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al actualizar el evento: {ex.Message}");
            }
        }
    }
}
