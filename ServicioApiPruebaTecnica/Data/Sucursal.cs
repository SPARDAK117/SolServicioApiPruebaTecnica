using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioApiPruebaTecnica.Data
{
    public class Sucursal
    {
            public int Id { get; set; }

            public string SucursalName { get; set; }

            public string Direccion { get; set; }

            public string Telefono { get; set; }

            public DateTime created_at { get; set; }

            public DateTime updated_at { get; set; }

        // Propiedad de navegación
        public ICollection<SucursalProducto> SucursalProductos { get; set; }
    }
}
