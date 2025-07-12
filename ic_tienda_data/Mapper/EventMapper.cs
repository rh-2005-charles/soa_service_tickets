using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public static class EventMapper
    {
        public static Event ToEntity(EventRequest request)
        {
            return new Event
            {
                Name = request.Name,
                Description = request.Description,
                Date = request.Date,
                Location = request.Location,
                Status = request.Status,
                ImageUrl = "img_placeholder.png"
            };
        }

        public static EventResponse ToResponse(Event entity)
        {
            return new EventResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Date = entity.Date,
                Status = entity.Status,
                Location = entity.Location,
                ImageUrl = entity.ImageUrl
            };
        }

        public static void UpdateEntity(Event entity, EventRequest request)
        {
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Date = request.Date;
            entity.Status = request.Status;
            entity.Location = request.Location;
            //entity.ImageUrl = request.ImgPath;
        }
    }
}