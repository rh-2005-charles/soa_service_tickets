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
                    FirstName = "Juan",
                    LastName = "Daba Daba Du",
                    Email = "cliente2@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "123456789",
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Maria",
                    LastName = "De la Vega",
                    Email = "cliente2@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "123456789",
                }
            );

            // Semilla para User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Pedrito",
                    LastName = "Pica Piedra",
                    Email = "admin@admin.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "123456789",
                    Role = "administrador",
                },
                new User
                {
                    Id = 2,
                    FirstName = "Lucho",
                    LastName = "Marmol",
                    Email = "vendedor@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "123456789",
                    Role = "vendedor",
                }
            );

            // Semilla para Events
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Concierto de Rock",
                    Description = "Concierto con las mejores bandas de rock nacional.",
                    Date = new DateTime(2025, 7, 15, 20, 0, 0),
                    Location = "Estadio Nacional, Lima",
                    ImageUrl = "concierto-rock.jpg",
                    Status = "active"
                },
                new Event
                {
                    Id = 2,
                    Name = "Festival de Jazz",
                    Description = "Festival internacional de jazz con artistas reconocidos",
                    Date = new DateTime(2025, 8, 10, 18, 0, 0),
                    Location = "Parque de la Exposición, Lima",
                    ImageUrl = "festival-jazz.jpg",
                    Status = "active"
                }
            );

            // Semilla para TicketTypes
            modelBuilder.Entity<TicketType>().HasData(
                // Tipos de ticket para el Concierto de Rock (EventId = 1)
                new TicketType
                {
                    Id = 1,
                    EventId = 1,
                    Name = "VIP",
                    Price = 150.00m,
                    Quantity = 50,
                    Description = "Asientos preferenciales cerca del escenario"
                },
                new TicketType
                {
                    Id = 2,
                    EventId = 1,
                    Name = "General",
                    Price = 80.00m,
                    Quantity = 200,
                    Description = "Asientos generales"
                },
                new TicketType
                {
                    Id = 3,
                    EventId = 1,
                    Name = "Estudiante",
                    Price = 50.00m,
                    Quantity = 100,
                    Description = "Para estudiantes con carnet vigente"
                },
                // Tipos de ticket para el Festival de Jazz (EventId = 2)
                new TicketType
                {
                    Id = 4,
                    EventId = 2,
                    Name = "Platinum",
                    Price = 200.00m,
                    Quantity = 30,
                    Description = "Acceso VIP con beneficios exclusivos"
                },
                new TicketType
                {
                    Id = 5,
                    EventId = 2,
                    Name = "Golden",
                    Price = 120.00m,
                    Quantity = 100,
                    Description = "Asientos preferenciales"
                }
            );

            // Semilla para Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = new DateTime(2025, 6, 1, 10, 30, 0),
                    TotalAmount = 310.00m, // 1 VIP + 2 Generales (150 + 80*2)
                    Status = "Pagado",
                    PaymentMethod = "Tarjeta",
                    TransactionId = 1001
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = new DateTime(2025, 6, 2, 11, 15, 0),
                    TotalAmount = 100.00m, // 2 Estudiantes (50*2)
                    Status = "Pagado",
                    PaymentMethod = "Yape",
                    TransactionId = 1002
                },
                new Order
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = new DateTime(2025, 6, 3, 14, 20, 0),
                    TotalAmount = 440.00m, // 1 Platinum + 2 Golden (200 + 120*2)
                    Status = "Pendiente",
                    PaymentMethod = "Transferencia",
                    TransactionId = 1003
                }
            );

            // Semilla para OrderDetails (que justifican los TotalAmount de las Orders)
            modelBuilder.Entity<OrderDetail>().HasData(
                // Detalles para Order 1 (Concierto de Rock)
                new OrderDetail
                {
                    Id = 1,
                    OrderId = 1,
                    TicketTypeId = 1, // 1 VIP
                    TicketTypeName = "VIP",
                    Quantity = 1,
                    SubTotal = 150.00m
                },
                new OrderDetail
                {
                    Id = 2,
                    OrderId = 1,
                    TicketTypeId = 2, // 2 Generales
                    TicketTypeName = "General",
                    Quantity = 2,
                    SubTotal = 160.00m
                },
                // Detalles para Order 2 (Concierto de Rock)
                new OrderDetail
                {
                    Id = 3,
                    OrderId = 2,
                    TicketTypeId = 3, // 2 Estudiantes
                    TicketTypeName = "Estudiante",
                    Quantity = 2,
                    SubTotal = 100.00m
                },
                // Detalles para Order 3 (Festival de Jazz)
                new OrderDetail
                {
                    Id = 4,
                    OrderId = 3,
                    TicketTypeId = 4, // 1 Platinum
                    TicketTypeName = "Platinum",
                    Quantity = 1,
                    SubTotal = 200.00m
                },
                new OrderDetail
                {
                    Id = 5,
                    OrderId = 3,
                    TicketTypeId = 5, // 2 Golden
                    TicketTypeName = "Golden",
                    Quantity = 2,
                    SubTotal = 240.00m
                }
            );

            // Semilla para Tickets (generados automáticamente basados en OrderDetails)
            modelBuilder.Entity<Ticket>().HasData(
                // Tickets para OrderDetail 1 (1 VIP)
                new Ticket
                {
                    Id = 1,
                    OrderDetailId = 1,
                    EventId = 1,
                    TicketTypeId = 1,
                    CustomerId = 1,
                    Status = "Activo",
                    SeatNumber = 101,
                    TicketUrl = "tickets/1.pdf",
                    QrCode = "QR123456"
                },
                // Tickets para OrderDetail 2 (2 Generales)
                new Ticket
                {
                    Id = 2,
                    OrderDetailId = 2,
                    EventId = 1,
                    TicketTypeId = 2,
                    CustomerId = 1,
                    Status = "Activo",
                    SeatNumber = 201,
                    TicketUrl = "tickets/2.pdf",
                    QrCode = "QR123457"
                },
                new Ticket
                {
                    Id = 3,
                    OrderDetailId = 2,
                    EventId = 1,
                    TicketTypeId = 2,
                    CustomerId = 1,
                    Status = "Activo",
                    SeatNumber = 202,
                    TicketUrl = "tickets/3.pdf",
                    QrCode = "QR123458"
                },
                // Tickets para OrderDetail 3 (2 Estudiantes)
                new Ticket
                {
                    Id = 4,
                    OrderDetailId = 3,
                    EventId = 1,
                    TicketTypeId = 3,
                    CustomerId = 1,
                    Status = "Activo",
                    SeatNumber = 301,
                    TicketUrl = "tickets/4.pdf",
                    QrCode = "QR123459"
                },
                new Ticket
                {
                    Id = 5,
                    OrderDetailId = 3,
                    EventId = 1,
                    TicketTypeId = 3,
                    CustomerId = 1,
                    Status = "Activo",
                    SeatNumber = 302,
                    TicketUrl = "tickets/5.pdf",
                    QrCode = "QR123460"
                },
                // Tickets para OrderDetail 4 (1 Platinum)
                new Ticket
                {
                    Id = 6,
                    OrderDetailId = 4,
                    EventId = 2,
                    TicketTypeId = 4,
                    CustomerId = 2,
                    Status = "Reservado",
                    SeatNumber = 501,
                    TicketUrl = "tickets/6.pdf",
                    QrCode = "QR123461"
                },
                // Tickets para OrderDetail 5 (2 Golden)
                new Ticket
                {
                    Id = 7,
                    OrderDetailId = 5,
                    EventId = 2,
                    TicketTypeId = 5,
                    CustomerId = 2,
                    Status = "Reservado",
                    SeatNumber = 601,
                    TicketUrl = "tickets/7.pdf",
                    QrCode = "QR123462"
                },
                new Ticket
                {
                    Id = 8,
                    OrderDetailId = 5,
                    EventId = 2,
                    TicketTypeId = 5,
                    CustomerId = 2,
                    Status = "Reservado",
                    SeatNumber = 602,
                    TicketUrl = "tickets/8.pdf",
                    QrCode = "QR123463"
                }
            );
        }


    }
}