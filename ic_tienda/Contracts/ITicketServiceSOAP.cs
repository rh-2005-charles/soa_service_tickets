using System.ServiceModel;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;

namespace ic_tienda.Contracts
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface ITicketServiceSOAP
    {
        [OperationContract]
        TicketPaginatedResponse GetAll(QueryObject query);

        [OperationContract]
        TicketResponse GetById(int id);

        [OperationContract]
        TicketResponse Add(TicketRequest request);

        [OperationContract]
        TicketResponse Update(int id, TicketRequest request);

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        List<TicketResponse> GetByOrderDetailId(int id);
    }
}