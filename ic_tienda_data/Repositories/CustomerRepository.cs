using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IRepositories;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IcTiendaDbContext _context;

        public CustomerRepository(IcTiendaDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerAuthResponse> Create(CustomerRegisterRequest request)
        {
            var customer = CustomerMapper.ToModel(request);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CustomerMapper.ToAuthResponse(customer, "", DateTime.MinValue);
        }

        public async Task<CustomerResponse?> GetByEmail(string email)
        {
            Console.WriteLine($"Buscando usuario por email: {email}");

            var customer = await _context.Customers
                .Where(c => c.Email == email)
                .Select(c => new CustomerResponse
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Password = c.Password,
                    Phone = c.Phone
                })
                .FirstOrDefaultAsync();

            Console.WriteLine(customer == null ? "Usuario no encontrado" : $"Usuario encontrado: {customer.Email}");

            return customer;
        }

    }
}