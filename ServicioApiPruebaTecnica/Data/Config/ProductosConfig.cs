using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServicioApiPruebaTecnica.Data.Config
{
    public class ProductosConfig : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProductoName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SKU).IsRequired().HasMaxLength(50);

            // Seed Data
            builder.HasData(new List<Producto>()
            {
                new Producto {
                    Id = 1,
                    ProductoName = "Producto A",
                    SKU = "A123",
                    created_at = DateTime.Now
                },
                new Producto {
                    Id = 2,
                    ProductoName = "Producto B",
                    SKU = "B456",
                    created_at = DateTime.Now
                },
                new Producto {
                    Id = 3,
                    ProductoName = "Producto C",
                    SKU = "C789",
                    created_at = DateTime.Now
                }
            });
        }
    }
}