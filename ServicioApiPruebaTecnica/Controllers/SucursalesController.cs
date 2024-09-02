using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServicioApiPruebaTecnica.Data;
using ServicioApiPruebaTecnica.Models;
using ServicioApiPruebaTecnica.Models.dataDTO;
using ServicioApiPruebaTecnica.MyLogging;
using ServicioApiPruebaTecnica.Services;
using System;

namespace ServicioApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly PruebaTecnicaOMCContextDB _dbContext;
        private readonly ILogService _logService;
        private readonly IMyLogger _logger;

        public SucursalesController(PruebaTecnicaOMCContextDB dbContext, ILogService logService, IMyLogger logger)
        {
            _dbContext = dbContext;
            _logService = logService;
            _logger = logger;
        }

        [HttpGet("All", Name = "BuscaTodasLasSucursales")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SucursalDTO>> GetAllSucursales()
        {
            _logger.Log("GetAllSucursales - Method started.");
            try
            {
                var sucursales = _dbContext.Sucursales.Select(s => new SucursalDTO()
                {
                    Id = s.Id,
                    SucursalName = s.SucursalName,
                    Direccion = s.Direccion,
                    Telefono = s.Telefono
                }).ToList();

                _logger.Log("GetAllSucursales - Successfully retrieved all branches.");
                return Ok(sucursales);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetAllSucursales: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpGet("{id:int}", Name = "GetSucursalById")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SucursalDTO> GetSucursalById(int id)
        {
            _logger.Log("GetSucursalById - Method started.");
            try
            {
                if (id <= 0)
                {
                    _logger.Log("GetSucursalById - Invalid ID provided.");
                    return BadRequest("Invalid ID.");
                }

                var sucursal = _dbContext.Sucursales.Where(n => n.Id == id).FirstOrDefault();

                if (sucursal == null)
                {
                    _logger.Log($"GetSucursalById - Sucursal with ID {id} not found.");
                    return NotFound($"La sucursal con el id {id} no fue encontrado");
                }

                var sucursalDTO = new SucursalDTO()
                {
                    Id = sucursal.Id,
                    SucursalName = sucursal.SucursalName,
                    Direccion = sucursal.Direccion,
                    Telefono = sucursal.Telefono
                };

                _logger.Log($"GetSucursalById - Successfully retrieved branch with ID {id}.");
                return Ok(sucursalDTO);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetSucursalById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpGet("{name:alpha}", Name = "BuscaSucursalPorNombre")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SucursalDTO> GetSucursalByName(string name)
        {
            _logger.Log("GetSucursalByName - Method started.");
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    _logger.Log("GetSucursalByName - Invalid name provided.");
                    return BadRequest("Invalid name.");
                }

                var sucursal = _dbContext.Sucursales.Where(n => n.SucursalName == name).FirstOrDefault();

                if (sucursal == null)
                {
                    _logger.Log($"GetSucursalByName - Sucursal with name {name} not found.");
                    return NotFound($"La sucursal con el nombre {name} no fue encontrado");
                }

                var sucursalDTO = new SucursalDTO()
                {
                    Id = sucursal.Id,
                    SucursalName = sucursal.SucursalName,
                    Direccion = sucursal.Direccion,
                    Telefono = sucursal.Telefono
                };

                _logger.Log($"GetSucursalByName - Successfully retrieved branch with name {name}.");
                return Ok(sucursalDTO);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in GetSucursalByName: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpPost]
        [Route("Crear")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SucursalDTO> CreateSucursal([FromBody] SucursalDTO model)
        {
            _logger.Log("CreateSucursal - Method started.");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Log("CreateSucursal - Invalid model state.");
                    return BadRequest(ModelState);
                }

                if (model == null)
                {
                    _logger.Log("CreateSucursal - Model is null.");
                    return BadRequest();
                }

                Sucursal sucursal = new Sucursal
                {
                    SucursalName = model.SucursalName,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    created_at = DateTime.Now,
                };

                _dbContext.Sucursales.Add(sucursal);
                _dbContext.SaveChanges();

                model.Id = sucursal.Id;

                _logger.Log($"CreateSucursal - Successfully created a new branch with ID {model.Id}.");
                return CreatedAtRoute("GetSucursalById", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in CreateSucursal: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the new branch.");
            }
        }

        [HttpPut]
        [Route("Editar")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SucursalDTO> UpdateSucursal([FromBody] SucursalDTO model)
        {
            _logger.Log("UpdateSucursal - Method started.");
            try
            {
                if (model == null || model.Id <= 0)
                {
                    _logger.Log("UpdateSucursal - Invalid model or ID.");
                    return BadRequest();
                }

                var SucursalExistente = _dbContext.Sucursales.Where(S => S.Id == model.Id).FirstOrDefault();

                if (SucursalExistente == null)
                {
                    _logger.Log($"UpdateSucursal - Sucursal with ID {model.Id} not found.");
                    return NotFound();
                }

                SucursalExistente.SucursalName = model.SucursalName;
                SucursalExistente.Direccion = model.Direccion;
                SucursalExistente.Telefono = model.Telefono;
                SucursalExistente.updated_at = DateTime.Now;
                _dbContext.SaveChanges();

                _logger.Log($"UpdateSucursal - Successfully updated branch with ID {model.Id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdateSucursal: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the branch.");
            }
        }

        [HttpPatch]
        [Route("{id:int}/Patch", Name = "EditarPartial")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SucursalDTO> UpdatePartialSucursal(int id, [FromBody] JsonPatchDocument<SucursalDTO> patchDocument)
        {
            _logger.Log("UpdatePartialSucursal - Method started.");
            try
            {
                if (patchDocument == null || id <= 0)
                {
                    _logger.Log("UpdatePartialSucursal - Invalid patch document or ID.");
                    return BadRequest();
                }

                var SucursalExistente = _dbContext.Sucursales.Where(S => S.Id == id).FirstOrDefault();

                if (SucursalExistente == null)
                {
                    _logger.Log($"UpdatePartialSucursal - Sucursal with ID {id} not found.");
                    return NotFound();
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
                    _logger.Log("UpdatePartialSucursal - Model state is invalid after applying patch.");
                    return BadRequest(ModelState);
                }

                SucursalExistente.SucursalName = sucursalDTO.SucursalName;
                SucursalExistente.Direccion = sucursalDTO.Direccion;
                SucursalExistente.Telefono = sucursalDTO.Telefono;
                SucursalExistente.updated_at = DateTime.Now;

                _dbContext.SaveChanges();
                _logger.Log($"UpdatePartialSucursal - Successfully patched branch with ID {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in UpdatePartialSucursal: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the branch.");
            }
        }

        [HttpDelete("Delete/{id:int}", Name = "EliminarSucursalPorId")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteSucursalByName(int id)
        {
            _logger.Log("DeleteSucursalByName - Method started.");
            try
            {
                if (id <= 0)
                {
                    _logger.Log("DeleteSucursalByName - Invalid ID provided.");
                    return BadRequest("Invalid ID.");
                }

                var sucursal = _dbContext.Sucursales.Where(n => n.Id == id).FirstOrDefault();

                if (sucursal == null)
                {
                    _logger.Log($"DeleteSucursalByName - Sucursal with ID {id} not found.");
                    return NotFound($"La sucursal con el Id {id} no fue encontrado");
                }

                _dbContext.Sucursales.Remove(sucursal);
                _dbContext.SaveChanges();

                _logger.Log($"DeleteSucursalByName - Successfully deleted branch with ID {id}.");
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in DeleteSucursalByName: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the branch.");
            }
        }
    }
}
