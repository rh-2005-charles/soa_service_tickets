using ic_tienda_data.sources.BaseDeDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.sources.BaseDeDatos.Seeds
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Semilla para Customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Carlos",
                    LastName = "Mendoza López",
                    Email = "carlos_mendoza@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "987654321",
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Ana",
                    LastName = "García Ruiz",
                    Email = "ana_garcia@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "912345678",
                }
            );

            // Semilla para User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Luis",
                    LastName = "Torres Sánchez",
                    Email = "admin@eventosperu.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "998877665",
                    Role = "Admin",
                },
                new User
                {
                    Id = 2,
                    FirstName = "María",
                    LastName = "Fernández Castro",
                    Email = "ventas@eventosperu.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "933445566",
                    Role = "Vendedor",
                }
            );

            // Semilla para Events
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Concierto de Rock",
                    Description = "Concierto con las mejores bandas de rock nacional.",
                    Date = new DateTime(2025, 8, 1, 18, 0, 0),
                    Location = "Ritmo y Sabor de Palián - Huancayo",
                    ImageUrl = "evento_1.jpg",
                    Status = "Activo"
                },
                new Event
                {
                    Id = 2,
                    Name = "Musica Latinoamericana",
                    Description = "Evento que reúne ritmos y culturas de América Latina en una celebración musical.",
                    Date = new DateTime(2025, 8, 20, 18, 0, 0),
                    Location = "Centro de Convenciones Shullkas - Huancayo",
                    ImageUrl = "evento_2.jpg",
                    Status = "Activo"
                }
            );

            // Semilla para TicketTypes
            modelBuilder.Entity<TicketType>().HasData(
                // Tipos de ticket para el Concierto de Rock (EventId = 1)
                new TicketType
                {
                    Id = 1,
                    EventId = 1,
                    Name = "Platinum",
                    Price = 80.00m,
                    Quantity = 100,
                    Description = "Acceso exclusivo con meet & greet con artistas."
                },
                new TicketType
                {
                    Id = 2,
                    EventId = 1,
                    Name = "VIP",
                    Price = 55.00m,
                    Quantity = 100,
                    Description = "Zona preferente con beneficios especiales."
                },
                new TicketType
                {
                    Id = 3,
                    EventId = 1,
                    Name = "General",
                    Price = 30.00m,
                    Quantity = 100,
                    Description = "Acceso estándar al área designada."
                },
                // Tipos de ticket para el Festival de Jazz (EventId = 2)
                new TicketType
                {
                    Id = 4,
                    EventId = 2,
                    Name = "Vip",
                    Price = 80.00m,
                    Quantity = 50,
                    Description = "Zona preferente con beneficios especiales."
                },
                new TicketType
                {
                    Id = 5,
                    EventId = 2,
                    Name = "General",
                    Price = 60.00m,
                    Quantity = 50,
                    Description = "Acceso estándar al área designada."
                }
            );

            // Semilla para Order
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = new DateTime(2025, 7, 12, 22, 12, 12, 276, DateTimeKind.Utc),
                    TotalAmount = 110.00m,
                    Status = "Finalizado",
                    PaymentMethod = "Yape",
                    TransactionId = 1
                }
            );

            // Semilla para OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    Id = 1,
                    OrderId = 1,
                    TicketTypeId = 2,
                    TicketTypeName = "VIP",
                    Quantity = 2,
                    SubTotal = 110.00m
                }
            );

            // Semilla para Tickets
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    OrderDetailId = 1,
                    EventId = 1,
                    TicketTypeId = 2,
                    CustomerId = 1,
                    Status = "Valido",
                    SeatNumber = 1,
                    TicketUrl = "ticket_1.png",
                    QrCode = "qrticket_1"
                },
                new Ticket
                {
                    Id = 2,
                    OrderDetailId = 1,
                    EventId = 1,
                    TicketTypeId = 2,
                    CustomerId = 1,
                    Status = "Valido",
                    SeatNumber = 2,
                    TicketUrl = "ticket_2.png",
                    QrCode = "qrticket_2"
                }
            );

        }
    }
}