using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServicioApiPruebaTecnica.Data;
using ServicioApiPruebaTecnica.Models;
using ServicioApiPruebaTecnica.Models.dataDTO;
using ServicioApiPruebaTecnica.MyLogging;
using System.Linq;

namespace ServicioApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly PruebaTecnicaOMCContextDB _dbContext;
        private readonly IMyLogger _logger;


        public ProductosController(PruebaTecnicaOMCContextDB dbContext, IMyLogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // GET: api/Productos/All
        [HttpGet("All", Name = "BuscaTodosLosProductos")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
    public ActionResult<IEnumerable<ProductoDTO>> GetAllProductos()
        {
            try
            {

                _logger.Log("GetAllProductos called.");
                var productos = _dbContext.Productos.Select(p => new ProductoDTO()
                {
                    Id = p.Id,
                    ProductoName = p.ProductoName,
                    SKU = p.SKU
                });

                _logger.Log($"{productos.Count()} sucursales found.");
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetAllProductos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        // GET: api/Productos/{id}
        [HttpGet("{id:int}", Name = "GetProductoById")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<ProductoDTO> GetProductoById(int id)
        {
            //BadRequest - 400 - Client Error
            if (id <= 0)
            {
                _logger.Log("Invalid ID parameter in GetProductoById.");
                return BadRequest("Invalid ID.");
            }
            try
            {
                _logger.Log($"GetProductoById called with ID: {id}.");
                var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

                //NotFound - 404 - Client error
                if (producto == null)
                {
                    _logger.Log($"Producto with ID {id} not found.");
                    return NotFound($"El producto con el id {id} no fue encontrado");
                }
                //creamos un nuevo objeto producto DTO para recibir la información obtenida
                var productoDTO = new ProductoDTO()
                {
                    Id = producto.Id,
                    ProductoName = producto.ProductoName,
                    SKU = producto.SKU
                };

                _logger.Log($"{productoDTO} producto found.");
                return Ok(productoDTO);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetProductoById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        // POST: api/Productos/Crear
        [HttpPost("Crear")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<ProductoDTO> CreateProducto([FromBody] ProductoDTO model)
        {
            //BadRequest - 400 - Client Error
            if (!ModelState.IsValid)
            {
                _logger.Log("Invalid model parameters in CreateProducto.");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.Log("CreateProducto called.");

                //BadRequest - 400 - Client error
                if (model == null)
                {
                    _logger.Log($"El modelo es null");
                    return BadRequest();
                }

                Producto producto = new Producto
                {
                    ProductoName = model.ProductoName,
                    SKU = model.SKU,
                    created_at = DateTime.Now
                };

                _dbContext.Productos.Add(producto);
                _dbContext.SaveChanges();

                model.Id = producto.Id;

                _logger.Log($"{producto} created correctly.");
                return CreatedAtRoute("GetProductoById", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetProductoById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        // PUT: api/Productos/Editar
        [HttpPut("Editar")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<ProductoDTO> UpdateProducto([FromBody] ProductoDTO model)
        {
            //BadRequest - 400 - Client Error
            if (model == null || model.Id <= 0)
            {
                _logger.Log("Invalid model parameters in UpdateProducto.");
                return BadRequest();
            }

            try
            {

                _logger.Log("UpdateProducto called.");

                var productoExistente = _dbContext.Productos.FirstOrDefault(p => p.Id == model.Id);

                //NotFound - 404 - Client error
                if (productoExistente == null)
                {
                    _logger.Log($"No se encontro el productp {productoExistente}");
                    return NotFound();
                }

                productoExistente.ProductoName = model.ProductoName;
                productoExistente.SKU = model.SKU;
                productoExistente.updated_at = DateTime.Now;

                _dbContext.SaveChanges();

                _logger.Log($"{productoExistente} updated correctly.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdateProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        // PATCH: api/Productos/{id}/Patch
        [HttpPatch("{id:int}/Patch", Name = "EditarPartialProducto")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<ProductoDTO> UpdatePartialProducto(int id, [FromBody] JsonPatchDocument<ProductoDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                _logger.Log("Invalid model parameters in UpdatePartialProducto.");
                return BadRequest();
            }
            try
            {
                _logger.Log("UpdatePartialProducto called.");
                var productoExistente = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

                if (productoExistente == null)
                {
                    _logger.Log($"No se encontro el producto {productoExistente}");
                    return NotFound();
                }

                var productoDTO = new ProductoDTO
                {
                    Id = productoExistente.Id,
                    ProductoName = productoExistente.ProductoName,
                    SKU = productoExistente.SKU
                };

                patchDocument.ApplyTo(productoDTO, ModelState);

                if (!ModelState.IsValid)
                {
                    _logger.Log($"No se valido el producto {productoExistente}");
                    return BadRequest(ModelState);
                }

                productoExistente.ProductoName = productoDTO.ProductoName;
                productoExistente.SKU = productoDTO.SKU;
                productoExistente.updated_at = DateTime.Now;

                _dbContext.SaveChanges();
                _logger.Log($"{productoExistente} patch correctly.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdatePartialProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        // DELETE: api/Productos/Delete/{id}
        [HttpDelete("Delete/{id:int}", Name = "EliminarProductoPorId")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<bool> DeleteProductoById(int id)
        {
            if (id <= 0)
            {
                _logger.Log($"Invalid {id} parameters in DeleteProductoById.");
                return BadRequest();
            }

            try
            {
                _logger.Log("DeleteProductoById called.");

                var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

                if (producto == null)
                {
                    _logger.Log($"No se encontro el producto {producto}");
                    return NotFound($"El producto con el Id {id} no fue encontrado");
                }

                _dbContext.Productos.Remove(producto);
                _dbContext.SaveChanges();

                _logger.Log($"{producto} deleted correctly.");
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in DeleteProductoById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }
    }
}
