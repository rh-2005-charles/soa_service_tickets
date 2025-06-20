using ic_tienda_data.sources.BaseDeDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.sources.BaseDeDatos.Seeds
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Semilla para la tabla Company
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Pedrito",
                    LastName = "Pica Piedra",
                    Email = "cliente1@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "123456789",
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Pedro",
                    LastName = "De la Vega",
                    Email = "cliente2@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "123456789",
                }
            );

            // Semilla para Events
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Aniversario Huancayo v2",
                    Description = "Viene grantes cantantes...",
                    Date = new DateTime(2025, 6, 20, 20, 30, 0),
                    Location = "Lima",
                    ImageUrl = "img.png",
                    Status = "active"
                },
                new Event
                {
                    Id = 2,
                    Name = "Aniversario Lima",
                    Description = "Viene grantes cantantes...",
                    Date = new DateTime(2025, 6, 21, 20, 30, 0),
                    Location = "Lima",
                    ImageUrl = "img.png",
                    Status = "active"
                }
            );

            // Semilla para TicketTypes
            modelBuilder.Entity<TicketType>().HasData(
                new TicketType
                {
                    Id = 1,
                    EventId = 1,
                    Name = "Tickeck Platino",
                    Price = 40.00m,
                    Quantity = 10,
                    Description = "Va a la izquierda superior"
                },
                new TicketType
                {
                    Id = 2,
                    EventId = 1,
                    Name = "Tickeck Vip",
                    Price = 30.00m,
                    Quantity = 10,
                    Description = "Va a la izquierda superior"
                },
                new TicketType
                {
                    Id = 3,
                    EventId = 1,
                    Name = "Tickeck General",
                    Price = 20.00m,
                    Quantity = 10,
                    Description = "Va a la izquierda superior"
                }
            );

            // Semilla para Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = new DateTime(2025, 6, 20, 20, 30, 0),
                    TotalAmount = 20.00m,
                    Status = "Pendiente",
                    PaymentMethod = "debito",
                    TransactionId = 1
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = new DateTime(2025, 7, 1, 19, 0, 0),
                    TotalAmount = 30.80m,
                    Status = "Pagado",
                    PaymentMethod = "Yape",
                    TransactionId = 2
                },
                new Order
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = new DateTime(2025, 6, 20, 20, 30, 0),
                    TotalAmount = 20.50m,
                    Status = "Pendiente",
                    PaymentMethod = "credito",
                    TransactionId = 3
                }
            );

            // Semilla para OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    Id = 1,
                    OrderId = 1,
                    TicketTypeId = 2,
                    Quantity = 1,
                    SubTotal = 10.00m
                },
                new OrderDetail
                {
                    Id = 2,
                    OrderId = 1,
                    TicketTypeId = 3,
                    Quantity = 2,
                    SubTotal = 30.00m
                }
            );
        }


    }
}