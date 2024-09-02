using Microsoft.EntityFrameworkCore;
using ServicioApiPruebaTecnica.Data.Config;

namespace ServicioApiPruebaTecnica.Data
{
    public class PruebaTecnicaOMCContextDB : DbContext
    {
        public PruebaTecnicaOMCContextDB(DbContextOptions<PruebaTecnicaOMCContextDB> options) : base(options)
        {
            
        }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<SucursalProducto> SucursalesProductos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SucursalesConfig());
            modelBuilder.ApplyConfiguration(new ProductosConfig());
            modelBuilder.ApplyConfiguration(new SucursalProductoConfig());
            modelBuilder.ApplyConfiguration(new UsuariosConfig());
            modelBuilder.ApplyConfiguration(new LogEntryConfig());


            base.OnModelCreating(modelBuilder);

        }
    }
}
