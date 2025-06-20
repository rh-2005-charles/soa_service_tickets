using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public static class TicketTypeMapper
    {
        public static TicketType ToEntity(TicketTypeRequest request)
        {
            return new TicketType
            {
                EventId = request.EventId,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                Description = request.Description
            };
        }

        public static TicketTypeResponse ToResponse(TicketType entity)
        {
            return new TicketTypeResponse
            {
                Id = entity.Id,
                EventId = entity.EventId,
                Name = entity.Name,
                Price = entity.Price,
                Quantity = entity.Quantity,
                Description = entity.Description
            };
        }

        public static void UpdateEntity(TicketType entity, TicketTypeRequest request)
        {
            entity.Name = request.Name;
            entity.EventId = request.EventId;
            entity.Name = request.Name;
            entity.Price = request.Price;
            entity.Quantity = request.Quantity;
            entity.Description = request.Description;
        }
    }
}