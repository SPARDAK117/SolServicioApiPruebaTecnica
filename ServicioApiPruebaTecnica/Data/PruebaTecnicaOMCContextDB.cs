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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SucursalesConfig());
            modelBuilder.ApplyConfiguration(new ProductosConfig());
            modelBuilder.ApplyConfiguration(new SucursalProductoConfig());

            base.OnModelCreating(modelBuilder);

        }
    }
}
