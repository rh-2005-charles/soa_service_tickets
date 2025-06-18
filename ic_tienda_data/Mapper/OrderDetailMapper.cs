using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public static class OrderDetailMapper
    {
        public static OrderDetailResponse OrderDetailResponseMap(this OrderDetail orderDetail)
        {
            return new OrderDetailResponse
            {
                Id = orderDetail.Id,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                ProductName = orderDetail.ProductName,
                Comment = orderDetail.Comment,
                ProductQuantity = orderDetail.ProductQuantity,
                ProductPrice = orderDetail.ProductPrice,
                SubTotal = orderDetail.SubTotal,
                Options = orderDetail.Options,
            };
        }

        public static OrderDetail ToOrderDetailMap(this OrderDetailRequest orderDetailRequest)
        {
            return new OrderDetail
            {
                OrderId = orderDetailRequest.OrderId,
                ProductId = orderDetailRequest.ProductId,
                ProductName = orderDetailRequest.ProductName,
                Comment = orderDetailRequest.Comment,
                ProductQuantity = orderDetailRequest.ProductQuantity,
                ProductPrice = orderDetailRequest.ProductPrice,
                SubTotal = orderDetailRequest.SubTotal,
                Options = orderDetailRequest.Options
            };
        }
    }
}