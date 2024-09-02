using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicioApiPruebaTecnica.Data;
using ServicioApiPruebaTecnica.Models;
using ServicioApiPruebaTecnica.Models.dataDTO;
using ServicioApiPruebaTecnica.MyLogging;

namespace ServicioApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SucursalesController : ControllerBase
    {

        private readonly PruebaTecnicaOMCContextDB _dbContext;
        //private readonly ILogService _logService;
        private readonly IMyLogger _logger;

        //Constructores
        public SucursalesController(PruebaTecnicaOMCContextDB dbContext, IMyLogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        //RespuestasAPI y Status Codes GetAllSucursales()
        #region 
        [HttpGet("All", Name = "BuscaTodasLasSucursales")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion

        //Método para recuperar todas las sucursales del sistema
        public ActionResult<IEnumerable<SucursalDTO>> GetAllSucursales()
        {
            try
            {
                _logger.Log("GetAllSucursales called.");
                var sucursales = _dbContext.Sucursales.Select(s => new SucursalDTO()
                {
                    Id = s.Id,
                    SucursalName = s.SucursalName,
                    Direccion = s.Direccion,
                    Telefono = s.Telefono
                });

                _logger.Log($"{sucursales.Count()} sucursales found.");

                //Ok - 200 Success
                return Ok(sucursales);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetAllSucursales: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        //RespuestasAPI y Status Codes GetSucursalById(int id)
        #region
        [HttpGet("{id:int}", Name = "GetSucursalById")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion

        //Método para recuperar la sucursal por su ID

        public ActionResult<SucursalDTO> GetSucursalById(int id)
        {
            //BadRequest - 400 - Client Error
            if (id <= 0)
            {
                _logger.Log("Invalid ID parameter in GetSucursalById.");
                return BadRequest("Invalid ID.");
            }

            try
            {
                _logger.Log($"GetSucursalById called with ID: {id}.");

                var sucursal = _dbContext.Sucursales.Where(n => n.Id == id).FirstOrDefault();

                //NotFound - 404 - Client error
                if (sucursal == null)
                {
                    _logger.Log($"Sucursal with ID {id} not found.");
                    return NotFound($"La sucursal con el id {id} no fue encontrada.");
                }

                var sucursalDTO = new SucursalDTO()
                {
                    Id = sucursal.Id,
                    SucursalName = sucursal.SucursalName,
                    Direccion = sucursal.Direccion,
                    Telefono = sucursal.Telefono
                };

                _logger.Log($"Sucursal with ID {id} retrieved successfully.");
                //Ok - 200 Success
                return Ok(sucursalDTO);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetSucursalById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
    
            }
        }

        //RespuestasAPI y Status Codes GetSucursalByName(string name)
        #region
        [HttpGet("{name:alpha}", Name = "BuscaSucursalPorNombre")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion

        //Método para encontrar Sucursal por Nombre
        public ActionResult<SucursalDTO> GetSucursalByName(string name)
        {
            //BadRequest - 400 - Client Error
            if (string.IsNullOrEmpty(name))
            {
                _logger.Log("Invalid name parameter in GetSucursalByName.");
                return BadRequest("Invalid name.");
            }

            try
            {
                _logger.Log($"GetSucursalByName called with name: {name}.");

                var sucursal = _dbContext.Sucursales.Where(n => n.SucursalName == name).FirstOrDefault();

                //NotFound - 404 - Client Error
                if (sucursal == null)
                {
                    _logger.Log($"Sucursal with name {name} not found.");
                    return NotFound($"La sucursal con el nombre {name} no fue encontrada.");
                }

                var sucursalDTO = new SucursalDTO()
                {
                    Id = sucursal.Id,
                    SucursalName = sucursal.SucursalName,
                    Direccion = sucursal.Direccion,
                    Telefono = sucursal.Telefono
                };

                _logger.Log($"Sucursal with name {name} retrieved successfully.");
                //Ok - 200
                return Ok(sucursalDTO);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetSucursalByName: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the data.");
            }
        }

        //RespuestasAPI y Status Codes CreateSucursal([FromBody] SucursalDTO model)
        #region
        [HttpPost]
        [Route("Crear")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion

        public ActionResult<SucursalDTO> CreateSucursal([FromBody] SucursalDTO model)
        {
            _logger.Log("CreateSucursal called.");

            if (!ModelState.IsValid)
            {
                _logger.Log("Invalid model state in CreateSucursal.");
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                _logger.Log("Null model in CreateSucursal.");
                return BadRequest("Invalid model.");
            }

            try
            {
                _logger.Log("CreateSucursal called.");

                Sucursal sucursal = new Sucursal
                {
                    SucursalName = model.SucursalName,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    created_at = DateTime.Now,

                };

                //Se crea nuevo registro de Sucursal
                _dbContext.Sucursales.Add(sucursal);
                _dbContext.SaveChanges();

                _logger.Log($"Sucursal created with ID {sucursal.Id}.");

                model.Id = sucursal.Id;


                _logger.Log($"Sucursal created with ID {model.Id}.");

                //Detalles de la nueva sucursal
                //https://localhost:7257/api/Sucursales/id
                //Ok - 201 
                return CreatedAtRoute("GetSucursalById", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {

                _logger.Log($"Error in CreateSucursal: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the sucursal.");
            }
        }


        //RespuestasAPI y Status Codes UpdateSucursal([FromBody]SucursalDTO model)
        #region
        [HttpPut]
        [Route("Editar")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        //Método para HttpPut UpdateSucursal([FromBody]SucursalDTO model)
        public ActionResult<SucursalDTO> UpdateSucursal([FromBody] SucursalDTO model)
        {
            //BadRequest - 400 - Client Error
            if (model == null || model.Id <= 0)
            {
                _logger.Log("Invalid model or ID in UpdateSucursal.");
                return BadRequest("Invalid model or ID.");
            }

            try
            {
                _logger.Log($"UpdateSucursal called with ID {model.Id}.");

                var SucursalExistente = _dbContext.Sucursales.Where(S => S.Id == model.Id).FirstOrDefault();

                //NotFound - 404 - Client Error
                if (SucursalExistente == null)
                {
                    _logger.Log($"Sucursal with ID {model.Id} not found.");
                    return NotFound($"La sucursal con el ID {model.Id} no fue encontrada.");
                }

                SucursalExistente.SucursalName = model.SucursalName;
                SucursalExistente.Direccion = model.Direccion;
                SucursalExistente.Telefono = model.Telefono;
                SucursalExistente.updated_at = DateTime.Now;
                _dbContext.SaveChanges();

                _logger.Log($"Sucursal with ID {model.Id} updated successfully.");
                //NoContent - 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdateSucursal: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the sucursal.");
            }
        }

        //RespuestasAPI y Status Codes UpdateSucursal([FromBody]SucursalDTO model)
        #region
        [HttpPatch]
        [Route("{id:int}/Patch", Name = "EditarPartial")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        //Método para HttpPut UpdateSucursal([FromBody]SucursalDTO model)
        public ActionResult<SucursalDTO> UpdatePartialSucursal(int id, [FromBody] JsonPatchDocument<SucursalDTO> patchDocument)
        {
            //BadRequest - 400 - Client Error
            if (patchDocument == null || id <= 0)
            {
                _logger.Log("Invalid patch document or ID in UpdatePartialSucursal.");
                return BadRequest("Invalid patch document or ID.");
            }

            try
            {
                _logger.Log($"UpdatePartialSucursal called with ID {id}.");

                var SucursalExistente = _dbContext.Sucursales.Where(S => S.Id == id).FirstOrDefault();

                //NotFound - 404 - Client Error
                if (SucursalExistente == null)
                {
                    _logger.Log($"Sucursal with ID {id} not found.");
                    return NotFound($"La sucursal con el ID {id} no fue encontrada.");
                }

                var sucursalDTO = new SucursalDTO
                {
                    Id = SucursalExistente.Id,
                    SucursalName = SucursalExistente.SucursalName,
                    Direccion = SucursalExistente.Direccion,
                    Telefono = SucursalExistente.Telefono
                };

                patchDocument.ApplyTo(sucursalDTO, ModelState);

                if (!ModelState.IsValid)
                {
                    _logger.Log("Invalid model state after applying patch in UpdatePartialSucursal.");
                    return BadRequest(ModelState);
                }

                SucursalExistente.SucursalName = sucursalDTO.SucursalName;
                SucursalExistente.Direccion = sucursalDTO.Direccion;
                SucursalExistente.Telefono = sucursalDTO.Telefono;
                SucursalExistente.updated_at = DateTime.Now;

                _dbContext.SaveChanges();

                _logger.Log($"Partial update for Sucursal with ID {id} completed successfully.");
                //NoContent - 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdatePartialSucursal: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the sucursal.");
            }
        }

        //RespuestasAPI y Status Codes CreateSucursal([FromBody] SucursalDTO model)
        #region
        [HttpDelete("Delete/{id:int}", Name = "EliminarSucursalPorId")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion

        //Método para eliminar Sucursales por su nombre
        public ActionResult<bool> DeleteSucursalByName(int id)
        {

            //BadRequest - 400 - Client Error
            if (id <= 0)
            {
                _logger.Log("Invalid ID in DeleteSucursalByName.");
                return BadRequest("Invalid ID.");
            }

            try
            {
                _logger.Log($"DeleteSucursalByName called with ID {id}.");

                var sucursal = _dbContext.Sucursales.Where(n => n.Id == id).FirstOrDefault();

                //NotFound - 404 - Client Error
                if (sucursal == null)
                {
                    _logger.Log($"Sucursal with ID {id} not found.");
                    return NotFound($"La sucursal con el ID {id} no fue encontrada.");
                }

                _dbContext.Sucursales.Remove(sucursal);
                _dbContext.SaveChanges();

                _logger.Log($"Sucursal with ID {id} deleted successfully.");
                //Ok - 200
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in DeleteSucursalByName: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the sucursal.");
            }
        }
    }
}
