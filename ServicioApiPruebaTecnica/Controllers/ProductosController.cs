using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: GET /api/Productos/GetAllProductos");
            _logger.Log("GetAllProductos - Method started.");
            try
            {
                var productos = _dbContext.Productos.Select(p => new ProductoDTO()
                {
                    Id = p.Id,
                    ProductoName = p.ProductoName,
                    SKU = p.SKU
                }).ToList();

                _logger.Log($"GetAllProductos - {productos.Count()} productos found.");
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
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: GET /api/Productos/GetProductoById");
            _logger.Log("GetProductoById - Method started.");
            if (id <= 0)
            {
                _logger.Log("GetProductoById - Invalid ID parameter.");
                return BadRequest("Invalid ID.");
            }

            try
            {
                var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

                if (producto == null)
                {
                    _logger.Log($"GetProductoById - Producto with ID {id} not found.");
                    return NotFound($"El producto con el id {id} no fue encontrado");
                }

                var productoDTO = new ProductoDTO()
                {
                    Id = producto.Id,
                    ProductoName = producto.ProductoName,
                    SKU = producto.SKU
                };

                _logger.Log($"GetProductoById - Producto with ID {id} found.");
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
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: POST /api/Productos/CreateProducto"); 
            _logger.Log("CreateProducto - Method started.");
            if (!ModelState.IsValid)
            {
                _logger.Log("CreateProducto - Invalid model state.");
                return BadRequest(ModelState);
            }

            try
            {
                if (model == null)
                {
                    _logger.Log("CreateProducto - Model is null.");
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

                _logger.Log($"CreateProducto - Producto created successfully with ID {model.Id}.");
                return CreatedAtRoute("GetProductoById", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in CreateProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the product.");
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
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: PUT /api/Productos/UpdateProducto");
            _logger.Log("UpdateProducto - Method started.");
            if (model == null || model.Id <= 0)
            {
                _logger.Log("UpdateProducto - Invalid model or ID.");
                return BadRequest();
            }

            try
            {
                var productoExistente = _dbContext.Productos.FirstOrDefault(p => p.Id == model.Id);

                if (productoExistente == null)
                {
                    _logger.Log($"UpdateProducto - Producto with ID {model.Id} not found.");
                    return NotFound();
                }

                productoExistente.ProductoName = model.ProductoName;
                productoExistente.SKU = model.SKU;
                productoExistente.updated_at = DateTime.Now;

                _dbContext.SaveChanges();

                _logger.Log($"UpdateProducto - Producto with ID {model.Id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdateProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product.");
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
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: PATCH /api/Productos/UpdatePartialProducto");
            _logger.Log("UpdatePartialProducto - Method started.");

            if (patchDocument == null || id <= 0)
            {
                _logger.Log("UpdatePartialProducto - Invalid patch document or ID.");
                return BadRequest();
            }

            try
            {
                var productoExistente = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

                if (productoExistente == null)
                {
                    _logger.Log($"UpdatePartialProducto - Producto with ID {id} not found.");
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
                    _logger.Log("UpdatePartialProducto - Model state is invalid after applying patch.");
                    return BadRequest(ModelState);
                }

                productoExistente.ProductoName = productoDTO.ProductoName;
                productoExistente.SKU = productoDTO.SKU;
                productoExistente.updated_at = DateTime.Now;

                _dbContext.SaveChanges();
                _logger.Log($"UpdatePartialProducto - Producto with ID {id} patched successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdatePartialProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while patching the product.");
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
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: DELETE /api/Productos/DeleteProductoById");
            _logger.Log("DeleteProductoById - Method started.");
            if (id <= 0)
            {
                _logger.Log("DeleteProductoById - Invalid ID parameter.");
                return BadRequest();
            }

            try
            {
                var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

                if (producto == null)
                {
                    _logger.Log($"DeleteProductoById - Producto with ID {id} not found.");
                    return NotFound($"El producto con el Id {id} no fue encontrado");
                }

                _dbContext.Productos.Remove(producto);
                _dbContext.SaveChanges();

                _logger.Log($"DeleteProductoById - Producto with ID {id} deleted successfully.");
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in DeleteProductoById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the product.");
            }
        }
    }
}
