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
                Events = result.Items,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };
        }

        /* public PaginatedResponse<EventResponse> GetAll(QueryObject query)
        {
            Console.WriteLine("[SOAP] Inicio de GetAll");

            if (query == null)
            {
                Console.WriteLine("[SOAP] Error: QueryObject es nulo");
                throw new FaultException("El objeto 'query' no puede ser nulo.");
            }

            Console.WriteLine($"[SOAP] Datos recibidos - PageNumber: {query.PageNumber}, PageSize: {query.PageSize}, Search: '{query.Search}'");

            try
            {
                Console.WriteLine("[SOAP] Llamando a _eventService.GetAllAsync");
                var result = _eventService.GetAllAsync(query).GetAwaiter().GetResult();
                Console.WriteLine("[SOAP] Llamada a servicio completada exitosamente");
                Console.WriteLine($"[SOAP] Resultado serializado: {System.Text.Json.JsonSerializer.Serialize(result)}");

                return result;

                //return _eventService.GetAllAsync(query).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SOAP] Error en GetAll: {ex.ToString()}");
                throw new FaultException($"Error interno: {ex.Message}");
            }
        }
 */
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
