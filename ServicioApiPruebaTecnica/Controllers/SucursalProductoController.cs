using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServicioApiPruebaTecnica.Data;
using ServicioApiPruebaTecnica.Models;
using ServicioApiPruebaTecnica.Models.dataDTO;
using ServicioApiPruebaTecnica.MyLogging;
using ServicioApiPruebaTecnica.Services;
using System.Linq;

namespace ServicioApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SucursalProductoController : ControllerBase
    {
        private readonly PruebaTecnicaOMCContextDB _dbContext;
        private readonly IMyLogger _logger;
        private readonly ILogService _logService;

        public SucursalProductoController(PruebaTecnicaOMCContextDB dbContext, ILogService logService, IMyLogger logger)
        {
            _dbContext = dbContext;
            _logService = logService;
            _logger = logger;
        }

        [HttpGet("All", Name = "GetAllSucursalProductos")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<IEnumerable<SucursalProductoDTO>> GetAllSucursalProductos()
        {
            try
            {
                var username = User.Identity?.Name ?? "Anonymous123";
                _logger.Log($"Request by {username}: GET /api/SucursalProducto/GetAllSucursalProductos");
                _logger.Log("GetAllSucursalProductos called.");

                var sucursalProductos = _dbContext.SucursalesProductos
                        .Select(sp => new SucursalProductoDTO
                        {
                            SucursalId = sp.SucursalId,
                            ProductoId = sp.ProductoId,
                            Cantidad = sp.Cantidad,
                            SucursalName = sp.Sucursal.SucursalName,
                            ProductoName = sp.Producto.ProductoName 
                        }).ToList();

                _logger.Log($"{sucursalProductos.Count()} inventario found.");
                return Ok(sucursalProductos);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetAllSucursalProductos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }


        [HttpGet("ProductoEnSucursalesPorNombre/{productoName}")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<IEnumerable<SucursalProductoDTO>> GetProductoEnSucursalesPorNombre(string productoName)
        {
            try
            {
                var username = User.Identity?.Name ?? "Anonymous123";
                _logger.Log($"Request by {username}: GET /api/SucursalProducto/GetProductoEnSucursalesPorNombre");
                _logger.Log("GetProductoEnSucursalesPorNombre called.");
                var productoEnSucursales = _dbContext.SucursalesProductos
                        .Where(sp => sp.Producto.ProductoName == productoName)
                        .Select(sp => new SucursalProductoDTO
                        {
                            SucursalId = sp.SucursalId,
                            SucursalName = sp.Sucursal.SucursalName, 
                            ProductoId = sp.ProductoId,
                            ProductoName = sp.Producto.ProductoName, 
                            Cantidad = sp.Cantidad
                        }).ToList();

                if (!productoEnSucursales.Any())
                {
                    _logger.Log($"Los producto en Sucursales with ID {productoName} not found.");
                    return NotFound("El producto no está disponible en ninguna sucursal.");
                }
                _logger.Log($"{productoEnSucursales.Count()} inventario found.");
                return Ok(productoEnSucursales);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetProductoEnSucursalesPorNombre: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        [HttpGet("PorSucursal/{sucursalId:int}")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<IEnumerable<SucursalProductoDTO>> GetProductosPorSucursal(int sucursalId)
        {
            try
            {
                var username = User.Identity?.Name ?? "Anonymous123";
                _logger.Log($"Request by {username}: GET /api/SucursalProducto/GetProductosPorSucursal");
                _logger.Log("GetProductosPorSucursal called.");

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

                _logger.Log($"{productosPorSucursal.Count()} inventario found.");
                return Ok(productosPorSucursal);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetProductosPorSucursal: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }
         
        [HttpGet("{sucursalId:int}/{productoId:int}", Name = "GetSucursalProductoById")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult<SucursalProductoDTO> GetSucursalProductoById(int sucursalId, int productoId)
        {

            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: GET /api/SucursalProducto/GetSucursalProductoById");

            if (sucursalId <= 0 || productoId <= 0)
            {
                _logger.Log("Invalid ID parameter in GetSucursalProductoById.");
                return BadRequest();
            }

            try
            {
                _logger.Log("GetSucursalProductoById called.");

                var sucursalProducto = _dbContext.SucursalesProductos.FirstOrDefault(sp => sp.SucursalId == sucursalId && sp.ProductoId == productoId);

                if (sucursalProducto == null)
                {
                    _logger.Log($"El producto {sucursalProducto} not found.");
                    return NotFound($"El producto con Id {productoId} en la sucursal con Id {sucursalId} no fue encontrado");
                }

                var sucursalProductoDTO = new SucursalProductoDTO
                {
                    SucursalId = sucursalProducto.SucursalId,
                    ProductoId = sucursalProducto.ProductoId,
                    Cantidad = sucursalProducto.Cantidad
                };

                _logger.Log($"{sucursalProductoDTO} producto found.");
                return Ok(sucursalProductoDTO);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetSucursalProductoById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        [HttpPost]
        #region
        [Route("Crear")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        
        public ActionResult<SucursalProductoDTO> CreateOrUpdateSucursalProducto([FromBody] PostInventarioDTO model)
        {
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: POST /api/SucursalProducto/CreateOrUpdateSucursalProducto");

            if (!ModelState.IsValid)
            {
                _logger.Log("Invalid model parameter in CreateOrUpdateSucursalProducto.");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.Log("CreateOrUpdateSucursalProducto called.");

                if (model == null)
                {
                    _logger.Log("El modelo es null.");
                    return BadRequest();
                }
                    

                var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == model.ProductoId);
                var sucursal = _dbContext.Sucursales.FirstOrDefault(s => s.Id == model.SucursalId);

                if (producto == null || sucursal == null)
                {
                    _logger.Log($"El producto o la sucursal fueron null");
                    return BadRequest("ProductoId o SucursalId no existe.");
                }


                var existente = _dbContext.SucursalesProductos
                    .FirstOrDefault(sp => sp.SucursalId == model.SucursalId && sp.ProductoId == model.ProductoId);

                if (existente != null)
                {
                    _logger.Log($"El registro existente es null");

                    existente.Cantidad = model.Cantidad;
                    existente.updated_at = DateTime.Now;
                    _dbContext.SaveChanges();
                    return NoContent(); 
                }

                var sucursalProducto = new SucursalProducto
                {
                    SucursalId = model.SucursalId,
                    ProductoId = model.ProductoId,
                    Cantidad = model.Cantidad,
                    created_at = DateTime.Now
                };

                _dbContext.SucursalesProductos.Add(sucursalProducto);
                _dbContext.SaveChanges();

                _logger.Log($"{sucursalProducto} producto found.");

                return CreatedAtRoute("GetSucursalProductoById", new { sucursalId = sucursalProducto.SucursalId, productoId = sucursalProducto.ProductoId }, model);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in CreateOrUpdateSucursalProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");

            }
        }


        
        [HttpPut]
        #region 
        [Route("Editar")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult UpdateSucursalProducto([FromBody] SucursalProductoDTO model)
        {
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: PUT /api/SucursalProducto/UpdateSucursalProducto");


            if (model == null || model.SucursalId <= 0 || model.ProductoId <= 0)
            {
                _logger.Log("Invalid model parameter in GetSucursalProductoById.");
                return BadRequest();
            }

            try
            {
                _logger.Log("UpdateSucursalProducto called.");

                var sucursalProducto = _dbContext.SucursalesProductos.FirstOrDefault(sp => sp.SucursalId == model.SucursalId && sp.ProductoId == model.ProductoId);

                if (sucursalProducto == null)
                {
                    _logger.Log($"SucursalProducto fue null o no se encontró");
                    return NotFound();
                }
                    

                sucursalProducto.Cantidad = model.Cantidad;
                sucursalProducto.updated_at = DateTime.Now;

                _dbContext.SaveChanges();

                _logger.Log($"{sucursalProducto} producto found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdateSucursalProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }


         
        [HttpPatch("{sucursalId:int}/{productoId:int}/Patch", Name = "UpdatePartialSucursalProducto")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult UpdatePartialSucursalProducto(int sucursalId, int productoId, [FromBody] JsonPatchDocument<SucursalProductoDTO> patchDocument)
        {
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: PATCH /api/SucursalProducto/UpdatePartialSucursalProducto");

            try
            {
                _logger.Log("UpdatePartialSucursalProducto called.");
                if (patchDocument == null || sucursalId <= 0 || productoId <= 0)
                {
                    _logger.Log("Invalid model parameter in UpdatePartialSucursalProducto.");

                    return BadRequest();

                }

                var sucursalProducto = _dbContext.SucursalesProductos.FirstOrDefault(sp => sp.SucursalId == sucursalId && sp.ProductoId == productoId);

                if (sucursalProducto == null)
                {
                    _logger.Log("Sucursal producto es null en UpdatePartialSucursalProducto.");

                    return NotFound();
                }
                    

                var sucursalProductoDTO = new SucursalProductoDTO
                {
                    SucursalId = sucursalProducto.SucursalId,
                    ProductoId = sucursalProducto.ProductoId,
                    Cantidad = sucursalProducto.Cantidad
                };

                patchDocument.ApplyTo(sucursalProductoDTO, ModelState);

                if (!ModelState.IsValid)
                {
                    _logger.Log($"model state invalid in UpdatePartialSucursalProducto");
                    return BadRequest(ModelState);
                }
                    

                sucursalProducto.Cantidad = sucursalProductoDTO.Cantidad;
                sucursalProducto.updated_at = DateTime.Now;

                _dbContext.SaveChanges();

                _logger.Log($"{sucursalProducto} producto found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdatePartialSucursalProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");

            }
        }


        [HttpDelete("Delete/{sucursalId:int}/{productoId:int}", Name = "DeleteSucursalProducto")]
        #region 
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult DeleteSucursalProducto(int sucursalId, int productoId)
        {
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: DELETE /api/SucursalProducto/DeleteSucursalProducto");

            try
            {
                _logger.Log("DeleteSucursalProducto called.");
                if (sucursalId <= 0 || productoId <= 0)
                {
                    _logger.Log("Invalid ID parameter in DeleteSucursalProducto.");

                    return BadRequest();
                }
                    

                var sucursalProducto = _dbContext.SucursalesProductos.FirstOrDefault(sp => sp.SucursalId == sucursalId && sp.ProductoId == productoId);

                if (sucursalProducto == null)
                    return NotFound();

                _dbContext.SucursalesProductos.Remove(sucursalProducto);
                _dbContext.SaveChanges();

                _logger.Log($"Sucursal producto with ID {sucursalId} deleted successfully.");

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in DeleteSucursalProducto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");

            }
        }

        [HttpDelete("DeleteByName/{sucursalName}/{productoName}")]
        #region
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public ActionResult DeleteSucursalProductoByName(string sucursalName, string productoName)
        {
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: DELETE /api/SucursalProducto/DeleteSucursalProductoByName");

            try
            {
                _logger.Log("DeleteSucursalProductoByName called.");

                var sucursalProducto = _dbContext.SucursalesProductos
                        .FirstOrDefault(sp => sp.Sucursal.SucursalName == sucursalName && sp.Producto.ProductoName == productoName);

                if (sucursalProducto == null)
                {
                    _logger.Log("Sucursal producto es null en DeleteSucursalProductoByName.");
                    return NotFound("No se encontró el registro de SucursalProducto con los nombres proporcionados.");
                }

                _dbContext.SucursalesProductos.Remove(sucursalProducto);
                _dbContext.SaveChanges();

                _logger.Log($"Sucursal producto with with {sucursalName} deleted successfully.");
                return Ok("Registro eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in DeleteSucursalProductoByName: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");

            }
        }
    }
}
