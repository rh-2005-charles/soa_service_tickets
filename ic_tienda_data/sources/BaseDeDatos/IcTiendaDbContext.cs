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
            //modelBuilder.Entity<Category>()
            //        .HasMany(p => p.Products)
            //        .WithOne(p => p.Category)
            //        .HasForeignKey(p => p.CategoryId)
            //        .IsRequired()
            //        .OnDelete(DeleteBehavior.Restrict);


            // Seeders
            modelBuilder.Seed();
        }
        //public DbSet<CategoryTable> categories { get; set; }
        // Propiedades que representan tablas de la base de datos.

        public DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }


        // Tabla de errores.
        public virtual DbSet<Error> Errors { get; set; }


        // Configuración del proveedor de base de datos y cadena de conexión.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuración para MySQL.
            var connectionString = _configuration.GetConnectionString("DbCloudMySQL");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
            x => x.MigrationsAssembly("ic_tienda"));

            // Configuración para SQLite.
            //var connectionString = _configuration.GetConnectionString("DbCloudSQLite");
            //optionsBuilder.UseSqlite(connectionString);

            // Para SQL Server
            //var connectionString = _configuration.GetConnectionString("DbCloudSQLServer");
            //optionsBuilder.UseSqlServer(connectionString);
        }
    }
}