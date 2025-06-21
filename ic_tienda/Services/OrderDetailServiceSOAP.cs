using CoreWCF;
using ic_tienda.Contracts;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;

namespace ic_tienda.Services
{
    [ServiceBehavior(Namespace = "http://tempuri.org/")]
    public class OrderDetailServiceSOAP : IOrderDetailServiceSOAP
    {
        private readonly IOrderDetailService _service;
        public OrderDetailServiceSOAP(IOrderDetailService service)
        {
            _service = service;
        }

        public OrderDetailResponse Add(OrderDetailRequest request)
        {
            try
            {
                return _service.AddAsync(request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al crear: {ex.Message}");
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
                throw new FaultException($"Error al eliminar: {ex.Message}");
            }
        }

        public OrderDetailPaginatedResponse GetAll(QueryObject query)
        {
            var result = _service.GetAllAsync(query).GetAwaiter().GetResult();

            return new OrderDetailPaginatedResponse
            {
                Items = result.Items,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };
        }

        public OrderDetailResponse GetById(int id)
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

        public List<OrderDetailResponse> GetByOrderId(int orderId)
        {
            try
            {
                return _service.GetByOrderIdAsync(orderId).GetAwaiter().GetResult();
            }
            catch
            {
                throw new FaultException($"Item con ID {orderId} no encontrado.");
            }
        }

        public OrderDetailResponse Update(int id, OrderDetailRequest request)
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