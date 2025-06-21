using System.ServiceModel;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda.Contracts
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface IEventServiceSOAP
    {
        [OperationContract]
        EventPaginatedResponse GetAll(QueryObject query);

        [OperationContract]
        EventResponse GetById(int id);

        [OperationContract]
        EventResponse Add(EventRequest request);

        [OperationContract]
        EventResponse Update(int id, EventRequest request);

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        void CancelEvent(int eventId);

    }
}