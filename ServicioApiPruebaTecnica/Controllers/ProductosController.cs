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
    public class ProductosController : ControllerBase
    {
        private readonly PruebaTecnicaOMCContextDB _dbContext;

        public ProductosController(PruebaTecnicaOMCContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Productos/All
        [HttpGet("All", Name = "BuscaTodosLosProductos")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ProductoDTO>> GetAllProductos()
        {
            var productos = _dbContext.Productos.Select(p => new ProductoDTO()
            {
                Id = p.Id,
                ProductoName = p.ProductoName,
                SKU = p.SKU
            });

            return Ok(productos);
        }

        // GET: api/Productos/{id}
        [HttpGet("{id:int}", Name = "GetProductoById")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductoDTO> GetProductoById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound($"El producto con el id {id} no fue encontrado");

            var productoDTO = new ProductoDTO()
            {
                Id = producto.Id,
                ProductoName = producto.ProductoName,
                SKU = producto.SKU
            };

            return Ok(productoDTO);
        }

        // POST: api/Productos/Crear
        [HttpPost("Crear")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductoDTO> CreateProducto([FromBody] ProductoDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest();

            Producto producto = new Producto
            {
                ProductoName = model.ProductoName,
                SKU = model.SKU,
                created_at = DateTime.Now
            };

            _dbContext.Productos.Add(producto);
            _dbContext.SaveChanges();

            model.Id = producto.Id;

            return CreatedAtRoute("GetProductoById", new { id = model.Id }, model);
        }

        // PUT: api/Productos/Editar
        [HttpPut("Editar")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductoDTO> UpdateProducto([FromBody] ProductoDTO model)
        {
            if (model == null || model.Id <= 0)
                return BadRequest();

            var productoExistente = _dbContext.Productos.FirstOrDefault(p => p.Id == model.Id);

            if (productoExistente == null)
                return NotFound();

            productoExistente.ProductoName = model.ProductoName;
            productoExistente.SKU = model.SKU;
            productoExistente.updated_at = DateTime.Now;

            _dbContext.SaveChanges();

            return NoContent();
        }

        // PATCH: api/Productos/{id}/Patch
        [HttpPatch("{id:int}/Patch", Name = "EditarPartialProducto")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductoDTO> UpdatePartialProducto(int id, [FromBody] JsonPatchDocument<ProductoDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var productoExistente = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

            if (productoExistente == null)
                return NotFound();

            var productoDTO = new ProductoDTO
            {
                Id = productoExistente.Id,
                ProductoName = productoExistente.ProductoName,
                SKU = productoExistente.SKU
            };

            patchDocument.ApplyTo(productoDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            productoExistente.ProductoName = productoDTO.ProductoName;
            productoExistente.SKU = productoDTO.SKU;
            productoExistente.updated_at = DateTime.Now;

            _dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Productos/Delete/{id}
        [HttpDelete("Delete/{id:int}", Name = "EliminarProductoPorId")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteProductoById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound($"El producto con el Id {id} no fue encontrado");

            _dbContext.Productos.Remove(producto);
            _dbContext.SaveChanges();
            return Ok(true);
        }
    }
}
