using System.ServiceModel;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda.Contracts
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface IOrderServiceSOAP
    {
        [OperationContract]
        OrderPaginatedResponse GetAll(QueryObject query);

        [OperationContract]
        OrderResponse GetById(int id);

        [OperationContract]
        OrderResponse Add(OrderRequest request);

        [OperationContract]
        OrderResponse Update(int id, OrderRequest request);

        [OperationContract]
        void Delete(int id);
    }
}