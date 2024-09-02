namespace ServicioApiPruebaTecnica.Data
{
    public class SucursalProducto
    {

        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
    }
}

