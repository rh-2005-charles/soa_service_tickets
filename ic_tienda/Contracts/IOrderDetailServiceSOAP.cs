using System.ServiceModel;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda.Contracts
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface IOrderDetailServiceSOAP
    {
        [OperationContract]
        OrderDetailPaginatedResponse GetAll(QueryObject query);

        [OperationContract]
        OrderDetailResponse GetById(int id);

        [OperationContract]
        OrderDetailResponse Add(OrderDetailRequest request);

        [OperationContract]
        OrderDetailResponse Update(int id, OrderDetailRequest request);

        [OperationContract]
        void Delete(int id);
    }
}