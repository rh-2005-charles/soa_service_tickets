using ic_tienda_data.sources.BaseDeDatos.Models;
using ic_tienda_data.sources.BaseDeDatos.Models.ActivityLogs;
using ic_tienda_data.sources.BaseDeDatos.Seeds;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ic_tienda_data.sources.BaseDeDatos
{
    public class IcTiendaDbContext : DbContext
    {
        // Acceso al contexto HTTP actual.
        private readonly IHttpContextAccessor _httpContextAccessor;
        // Configuraciones de la aplicación.
        private readonly IConfiguration _configuration;

        public IcTiendaDbContext(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public IcTiendaDbContext(DbContextOptions<IcTiendaDbContext> options,
            IHttpContextAccessor httpContextAccessor = null,
            IConfiguration configuration = null) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


        // Sobrescribe SaveChangesAsync para establecer campos de auditoría antes de guardar.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Establece los campos de auditoría.
            // SetAuditFields();
            // Llama al método base.
            return await base.SaveChangesAsync(cancellationToken);
        }

        // Sobrescribe SaveChanges para incluir auditoría en operaciones síncronas.
        public override int SaveChanges()
        {
            // Establece los campos de auditoría.
            //SetAuditFields();
            // Llama al método base.
            return base.SaveChanges();
        }

        // Obtiene el ID del usuario actual a partir del contexto HTTP.
        private int GetCurrentUserId()
        {
            // Busca el claim "sub".
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            // Devuelve el ID o 0 si no es válido.
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Relacion de 1 a muchos de Product y Categoria
            modelBuilder.Entity<Event>()
                .HasMany(t => t.TicketTypes)
                .WithOne(t => t.Event)
                .HasForeignKey(t => t.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(c => c.OrderDetails)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketType>()
                .HasMany(c => c.OrderDetails)
                .WithOne(c => c.TicketType)
                .HasForeignKey(c => c.TicketTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasMany(t => t.Tickets)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasMany(t => t.Tickets)
                .WithOne(t => t.Event)
                .HasForeignKey(t => t.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasMany(t => t.Tickets)
                .WithOne(t => t.OrderDetail)
                .HasForeignKey(t => t.OrderDetailId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketType>()
                .HasMany(t => t.Tickets)
                .WithOne(t => t.TicketType)
                .HasForeignKey(t => t.TicketTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Seeders
            modelBuilder.Seed();
        }
        // Propiedades que representan tablas de la base de datos.

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<User> Users { get; set; }





        // Tabla de errores.
        // public virtual DbSet<Error> Errors { get; set; }


        // Configuración del proveedor de base de datos y cadena de conexión.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuración para MySQL.
            var connectionString = _configuration.GetConnectionString("DbCloudMySQL");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
            x => x.MigrationsAssembly("ic_tienda"));
        }
    }
}