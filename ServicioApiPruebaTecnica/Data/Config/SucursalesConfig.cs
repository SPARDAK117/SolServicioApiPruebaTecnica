using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServicioApiPruebaTecnica.Data.Config
{
    public class SucursalesConfig : IEntityTypeConfiguration<Sucursal>
    {
        public void Configure(EntityTypeBuilder<Sucursal> builder)
        {
            builder.ToTable("Sucursales");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.SucursalName).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Direccion).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Telefono).IsRequired().HasMaxLength(10);

            builder.HasData(new List<Sucursal>()
            {
                new Sucursal {
                    Id = 1,
                    SucursalName = "Xola",
                    Direccion = "Calle Xola 23#",
                    Telefono = "5546354636",
                    created_at = DateTime.Now
                },
                new Sucursal {
                    Id = 2,
                    SucursalName = "Chilpancigo",
                    Direccion = "Calle Chilpanginco 23#",
                    Telefono = "5532165487",
                    created_at = DateTime.Now
                }
            });
        }
    }
}
