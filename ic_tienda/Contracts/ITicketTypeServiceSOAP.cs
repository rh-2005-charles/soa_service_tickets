using System.ServiceModel;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda.Contracts
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface ITicketTypeServiceSOAP
    {
        [OperationContract]
        TicketTypePaginatedResponse GetAll(QueryObject query);

        [OperationContract]
        TicketTypeResponse GetById(int id);

        [OperationContract]
        TicketTypeResponse Add(TicketTypeRequest request);

        [OperationContract]
        TicketTypeResponse Update(int id, TicketTypeRequest request);

        [OperationContract]
        void Delete(int id);
    }
}