namespace ServicioApiPruebaTecnica.Data
{
    public class Producto
    {
        public int Id { get; set; }

        public string ProductoName { get; set; }

        public string SKU { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public DateTime deleted_at { get; set; }

        // Propiedad de navegación
        public ICollection<SucursalProducto> SucursalProductos { get; set; }
    }
}

