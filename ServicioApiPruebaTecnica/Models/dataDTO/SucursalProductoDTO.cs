using ServicioApiPruebaTecnica.Data;

namespace ServicioApiPruebaTecnica.Models.dataDTO
{
    public class SucursalProductoDTO
    {
        public int SucursalId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string SucursalName { get; set; } // Nueva propiedad para el nombre de la sucursal
        public string ProductoName { get; set; } // Nueva propiedad para el nombre del producto
    }
}
