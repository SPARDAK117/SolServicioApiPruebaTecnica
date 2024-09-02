using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServicioApiPruebaTecnica.Data;
using ServicioApiPruebaTecnica.Models;
using ServicioApiPruebaTecnica.Models.dataDTO;

namespace ServicioApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SucursalesController : ControllerBase
    {

        private readonly PruebaTecnicaOMCContextDB _dbContext;

        public SucursalesController(PruebaTecnicaOMCContextDB dbContext)
        {
            _dbContext = dbContext;
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
            var sucursales = _dbContext.Sucursales.Select(s => new SucursalDTO()
            {
                Id = s.Id,
                SucursalName = s.SucursalName,
                Direccion = s.Direccion,
                Telefono = s.Telefono
            });

            //Ok - 200 Success
            return Ok(sucursales);
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
                return BadRequest();

            var sucursal = _dbContext.Sucursales.Where(n => n.Id == id).FirstOrDefault();

            //NotFound - 404 - Client error
            if (sucursal == null)
                return NotFound($"La sucursal con el id {id} no fue encontrado");

            var sucursalDTO = new SucursalDTO()
            {
                Id = sucursal.Id,
                SucursalName = sucursal.SucursalName,
                Direccion = sucursal.Direccion,
                Telefono = sucursal.Telefono
            };

            //Ok - 200 Success
            return Ok(sucursalDTO);
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
                return BadRequest();

            var sucursal = _dbContext.Sucursales.Where(n => n.SucursalName == name).FirstOrDefault();

            //NotFound - 404 - Client Error
            if (sucursal == null)
                return NotFound($"La sucursal con el nombre {name} no fue encontrado");

            var sucursalDTO = new SucursalDTO()
            {
                Id = sucursal.Id,
                SucursalName = sucursal.SucursalName,
                Direccion = sucursal.Direccion,
                Telefono = sucursal.Telefono
            };

            //Ok - 200
            return Ok(sucursalDTO);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest();

            //int newId = _dbContext.Sucursales.LastOrDefault().Id + 1;

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

            model.Id = sucursal.Id;

            //Ok - 201 - Created
            //Detalles de la nueva sucursal
            //https://localhost:7257/api/Sucursales/id
            return CreatedAtRoute("GetSucursalById", new { id = model.Id }, model);
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
                return BadRequest();

            var SucursalExistente = _dbContext.Sucursales.Where(S => S.Id == model.Id).FirstOrDefault();

            //NotFound - 404 - Client Error
            if (SucursalExistente == null)
                return NotFound();

            SucursalExistente.SucursalName = model.SucursalName;
            SucursalExistente.Direccion = model.Direccion;
            SucursalExistente.Telefono = model.Telefono;
            SucursalExistente.updated_at = DateTime.Now;
            _dbContext.SaveChanges();

            //NoContent - 204
            return NoContent();
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
                return BadRequest();

            var SucursalExistente = _dbContext.Sucursales.Where(S => S.Id == id).FirstOrDefault();

            //NotFound - 404 - Client Error
            if (SucursalExistente == null)
                return NotFound();

            var sucursalDTO = new SucursalDTO
            {
                Id = SucursalExistente.Id,
                SucursalName = SucursalExistente.SucursalName,
                Direccion = SucursalExistente.Direccion,
                Telefono = SucursalExistente.Telefono
            };

            patchDocument.ApplyTo(sucursalDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            SucursalExistente.SucursalName = sucursalDTO.SucursalName;
            SucursalExistente.Direccion = sucursalDTO.Direccion;
            SucursalExistente.Telefono = sucursalDTO.Telefono;
            SucursalExistente.updated_at = DateTime.Now;

            _dbContext.SaveChanges();
            //NoContent - 204
            return NoContent();
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
                return BadRequest();

            var sucursal = _dbContext.Sucursales.Where(n => n.Id == id).FirstOrDefault();

            //NotFound - 404 - Client Error
            if (sucursal == null)
                return NotFound($"La sucursal con el Id {id} no fue encontrado");

            _dbContext.Sucursales.Remove(sucursal);
            _dbContext.SaveChanges();
            //Ok - 200
            return Ok(true);
        }
    }
}
