using System.ComponentModel.DataAnnotations;

namespace ServicioApiPruebaTecnica.Models.dataDTO
{
    public class SucursalDTO
    {
        
            public int Id { get; set; }

            public string SucursalName { get; set; }
   
            public string Direccion { get; set; }
  
            public string Telefono { get; set; }    
    }

}



