using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServicioApiPruebaTecnica.Data;
using ServicioApiPruebaTecnica.Models;
using ServicioApiPruebaTecnica.Models.dataDTO;
using System.Linq;

namespace ServicioApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalProductoController : ControllerBase
    {
        private readonly PruebaTecnicaOMCContextDB _dbContext;

        public SucursalProductoController(PruebaTecnicaOMCContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/SucursalProducto/All
        [HttpGet("All", Name = "GetAllSucursalProductos")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SucursalProductoDTO>> GetAllSucursalProductos()
        {
            var sucursalProductos = _dbContext.SucursalesProductos
                .Select(sp => new SucursalProductoDTO
                {
                    SucursalId = sp.SucursalId,
                    ProductoId = sp.ProductoId,
                    Cantidad = sp.Cantidad,
                    SucursalName = sp.Sucursal.SucursalName, // Agrega el nombre de la sucursal
                    ProductoName = sp.Producto.ProductoName  // Agrega el nombre del producto
                }).ToList();

            return Ok(sucursalProductos);
        }


        [HttpGet("ProductoEnSucursalesPorNombre/{productoName}")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SucursalProductoDTO>> GetProductoEnSucursalesPorNombre(string productoName)
        {
            var productoEnSucursales = _dbContext.SucursalesProductos
                .Where(sp => sp.Producto.ProductoName == productoName)
                .Select(sp => new SucursalProductoDTO
                {
                    SucursalId = sp.SucursalId,
                    SucursalName = sp.Sucursal.SucursalName,  // Nombre de la Sucursal
                    ProductoId = sp.ProductoId,
                    ProductoName = sp.Producto.ProductoName,  // Nombre del Producto
                    Cantidad = sp.Cantidad
                }).ToList();

            if (!productoEnSucursales.Any())
            {
                return NotFound("El producto no está disponible en ninguna sucursal.");
            }

            return Ok(productoEnSucursales);
        }











        [HttpGet("PorSucursal/{sucursalId:int}")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SucursalProductoDTO>> GetProductosPorSucursal(int sucursalId)
        {
            var productosPorSucursal = _dbContext.SucursalesProductos
                .Where(sp => sp.SucursalId == sucursalId)
                .Select(sp => new SucursalProductoDTO
                {
                    SucursalId = sp.SucursalId,
                    ProductoId = sp.ProductoId,
                    Cantidad = sp.Cantidad,
                    SucursalName = sp.Sucursal.SucursalName,
                    ProductoName = sp.Producto.ProductoName
                }).ToList();

            return Ok(productosPorSucursal);
        }
        // GET: api/SucursalProducto/{sucursalId}/{productoId}
        #region 
        [HttpGet("{sucursalId:int}/{productoId:int}", Name = "GetSucursalProductoById")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<SucursalProductoDTO> GetSucursalProductoById(int sucursalId, int productoId)
        {
            if (sucursalId <= 0 || productoId <= 0)
                return BadRequest();

            var sucursalProducto = _dbContext.SucursalesProductos.FirstOrDefault(sp => sp.SucursalId == sucursalId && sp.ProductoId == productoId);

            if (sucursalProducto == null)
                return NotFound($"El producto con Id {productoId} en la sucursal con Id {sucursalId} no fue encontrado");

            var sucursalProductoDTO = new SucursalProductoDTO
            {
                SucursalId = sucursalProducto.SucursalId,
                ProductoId = sucursalProducto.ProductoId,
                Cantidad = sucursalProducto.Cantidad
            };

            return Ok(sucursalProductoDTO);
        }

        // POST: api/SucursalProducto/Crear
        #region 
        [HttpPost]
        [Route("Crear")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        
        public ActionResult<SucursalProductoDTO> CreateOrUpdateSucursalProducto([FromBody] PostInventarioDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest();

            // Verificar si el ProductoId y SucursalId existen en sus respectivas tablas
            var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == model.ProductoId);
            var sucursal = _dbContext.Sucursales.FirstOrDefault(s => s.Id == model.SucursalId);

            if (producto == null || sucursal == null)
            {
                return BadRequest("ProductoId o SucursalId no existe.");
            }

            // Buscar un registro existente con la combinación SucursalId y ProductoId
            var existente = _dbContext.SucursalesProductos
                .FirstOrDefault(sp => sp.SucursalId == model.SucursalId && sp.ProductoId == model.ProductoId);

            if (existente != null)
            {
                // Si existe, actualizar el registro existente
                existente.Cantidad = model.Cantidad;
                existente.updated_at = DateTime.Now;
                _dbContext.SaveChanges();
                return NoContent(); // 204 No Content, porque se ha actualizado el registro existente
            }

            // Si no existe, crear un nuevo registro
            var sucursalProducto = new SucursalProducto
            {
                SucursalId = model.SucursalId,
                ProductoId = model.ProductoId,
                Cantidad = model.Cantidad,
                created_at = DateTime.Now
            };

            _dbContext.SucursalesProductos.Add(sucursalProducto);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetSucursalProductoById", new { sucursalId = sucursalProducto.SucursalId, productoId = sucursalProducto.ProductoId }, model);
        }



        // PUT: api/SucursalProducto/Editar
        #region 
        [HttpPut]
        [Route("Editar")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult UpdateSucursalProducto([FromBody] SucursalProductoDTO model)
        {
            if (model == null || model.SucursalId <= 0 || model.ProductoId <= 0)
                return BadRequest();

            var sucursalProducto = _dbContext.SucursalesProductos.FirstOrDefault(sp => sp.SucursalId == model.SucursalId && sp.ProductoId == model.ProductoId);

            if (sucursalProducto == null)
                return NotFound();

            sucursalProducto.Cantidad = model.Cantidad;
            sucursalProducto.updated_at = DateTime.Now;

            _dbContext.SaveChanges();

            return NoContent();
        }

        // PATCH: api/SucursalProducto/{sucursalId}/{productoId}/Patch
        #region 
        [HttpPatch("{sucursalId:int}/{productoId:int}/Patch", Name = "UpdatePartialSucursalProducto")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult UpdatePartialSucursalProducto(int sucursalId, int productoId, [FromBody] JsonPatchDocument<SucursalProductoDTO> patchDocument)
        {
            if (patchDocument == null || sucursalId <= 0 || productoId <= 0)
                return BadRequest();

            var sucursalProducto = _dbContext.SucursalesProductos.FirstOrDefault(sp => sp.SucursalId == sucursalId && sp.ProductoId == productoId);

            if (sucursalProducto == null)
                return NotFound();

            var sucursalProductoDTO = new SucursalProductoDTO
            {
                SucursalId = sucursalProducto.SucursalId,
                ProductoId = sucursalProducto.ProductoId,
                Cantidad = sucursalProducto.Cantidad
            };

            patchDocument.ApplyTo(sucursalProductoDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            sucursalProducto.Cantidad = sucursalProductoDTO.Cantidad;
            sucursalProducto.updated_at = DateTime.Now;

            _dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE: api/SucursalProducto/Delete/{sucursalId}/{productoId}
        #region 
        [HttpDelete("Delete/{sucursalId:int}/{productoId:int}", Name = "DeleteSucursalProducto")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult DeleteSucursalProducto(int sucursalId, int productoId)
        {
            if (sucursalId <= 0 || productoId <= 0)
                return BadRequest();

            var sucursalProducto = _dbContext.SucursalesProductos.FirstOrDefault(sp => sp.SucursalId == sucursalId && sp.ProductoId == productoId);

            if (sucursalProducto == null)
                return NotFound();

            _dbContext.SucursalesProductos.Remove(sucursalProducto);
            _dbContext.SaveChanges();

            return Ok(true);
        }

        [HttpDelete("DeleteByName/{sucursalName}/{productoName}")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteSucursalProductoByName(string sucursalName, string productoName)
        {
            var sucursalProducto = _dbContext.SucursalesProductos
                .FirstOrDefault(sp => sp.Sucursal.SucursalName == sucursalName && sp.Producto.ProductoName == productoName);

            if (sucursalProducto == null)
                return NotFound("No se encontró el registro de SucursalProducto con los nombres proporcionados.");

            _dbContext.SucursalesProductos.Remove(sucursalProducto);
            _dbContext.SaveChanges();

            return Ok("Registro eliminado exitosamente.");
        }
    }
}
