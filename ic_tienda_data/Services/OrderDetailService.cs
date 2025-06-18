using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<OrderDetailResponse> AddAsync(OrderDetailRequest orderDetailRequest)
        {
            return await _orderDetailRepository.AddAsync(orderDetailRequest);
        }

        public async Task<OrderDetailResponse> GetByIdAsync(int id)
        {
            return await _orderDetailRepository.GetByIdAsync(id);
        }
    }
}