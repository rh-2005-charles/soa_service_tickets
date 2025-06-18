using ic_tienda.Contracts;
using ic_tienda.Services;
using ic_tienda_business.Dtos.Otros;
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

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();


            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAuthCustomerService, AuthCustomerService>();

            services.AddScoped<CustomerAuthServiceSOAP>();

            return services;
        }
    }
}
