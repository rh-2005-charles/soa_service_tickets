using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public static class OrderMapper
    {
        public static OrderResponse OrderResponseMap(this Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                Date = order.Date,
                PaymentType = order.PaymentType,
                ShippingType = order.ShippingType,
                ReceiptType = order.ReceiptType,
                PriceShipping = order.PriceShipping,
                TotalAmount = order.TotalAmount,
                CustomerId = order.CustomerId,
                State = order.State,
                MessageStatus = order.MessageStatus,
                Location = order.Location,
                Reference = order.Reference,
                Lat = order.Lat,
                Lng = order.Lng,
                ImgPath = order.ImgUrl
            };
        }

        public static Order ToEntity(OrderRequest orderRequest)
        {
            return new Order
            {
                Date = orderRequest.Date,
                PaymentType = orderRequest.PaymentType,
                ShippingType = orderRequest.ShippingType,
                ReceiptType = orderRequest.ReceiptType,
                PriceShipping = orderRequest.PriceShipping,
                TotalAmount = orderRequest.TotalAmount,
                CustomerId = orderRequest.CustomerId,
                State = orderRequest.State,
                MessageStatus = orderRequest.MessageStatus,
                Location = orderRequest.Location,
                Reference = orderRequest.Reference,
                Lat = orderRequest.Lat,
                Lng = orderRequest.Lng,
                //ImgUrl = orderRequest.ImgPath
            };
        }

        public static void UpdateEntity(Order order, OrderRequest orderRequest)
        {
            order.Date = orderRequest.Date;
            order.PaymentType = orderRequest.PaymentType;
            order.ShippingType = orderRequest.ShippingType;
            order.ReceiptType = orderRequest.ReceiptType;
            order.PriceShipping = orderRequest.PriceShipping;
            order.TotalAmount = orderRequest.TotalAmount;
            order.State = orderRequest.State;
            order.MessageStatus = orderRequest.MessageStatus;
            order.Location = orderRequest.Location;
            order.Reference = orderRequest.Reference;
            order.Lat = orderRequest.Lat;
            order.Lng = orderRequest.Lng;
        }
    }
}