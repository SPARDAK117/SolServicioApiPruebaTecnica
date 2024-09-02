using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServicioApiPruebaTecnica.Data.Config
{
    public class SucursalProductoConfig : IEntityTypeConfiguration<SucursalProducto>
    {
        public void Configure(EntityTypeBuilder<SucursalProducto> builder)
        {
            builder.ToTable("SucursalProductos");
            builder.HasKey(sp => new { sp.SucursalId, sp.ProductoId }); // Clave compuesta

            builder.HasOne(sp => sp.Sucursal)
                   .WithMany(s => s.SucursalProductos)
                   .HasForeignKey(sp => sp.SucursalId);

            builder.HasOne(sp => sp.Producto)
                   .WithMany(p => p.SucursalProductos)
                   .HasForeignKey(sp => sp.ProductoId);

            builder.Property(sp => sp.Cantidad).IsRequired();
        }
    }
}