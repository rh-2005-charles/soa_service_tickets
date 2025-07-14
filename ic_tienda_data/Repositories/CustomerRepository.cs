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

        public async Task<bool> CancelOrderAsync(int orderId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Obtener la orden
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Tickets)
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null)
                    return false;

                // 2. Cambiar estado de la orden a "Cancelado"
                order.Status = "Cancelado";

                // 3. Cambiar estado de todos los tickets asociados
                foreach (var orderDetail in order.OrderDetails)
                {
                    foreach (var ticket in orderDetail.Tickets)
                    {
                        ticket.Status = "Cancelado";
                    }
                }

                // 4. Guardar cambios
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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

        public async Task<CustomerResponse> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return null;

            return CustomerMapper.ToResponse(customer);
        }

    }
}