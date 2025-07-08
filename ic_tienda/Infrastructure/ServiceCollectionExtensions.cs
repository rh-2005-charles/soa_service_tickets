using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;
using ic_tienda_business.IServices.Images;
using ic_tienda_data.Repositories;
using ic_tienda_data.Services;
using ic_tienda_data.Services.Image;

namespace ic_tienda.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            // Inyeccion de dependencias
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();

            // Token
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // Customer
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAuthCustomerService, AuthCustomerService>();

            // User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthUserService, AuthUserService>();

            // Event
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();

            // TicketType
            services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
            services.AddScoped<ITicketTypeService, TicketTypeService>();

            // Ticket
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();

            // Order
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            // OrderDetail
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();

            return services;
        }
    }
}
