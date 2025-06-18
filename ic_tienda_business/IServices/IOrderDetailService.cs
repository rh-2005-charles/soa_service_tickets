using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IServices
{
    public interface IOrderDetailService
    {
         Task<OrderDetailResponse> GetByIdAsync(int id);
        Task<OrderDetailResponse> AddAsync(OrderDetailRequest orderDetailRequest);
    }
}