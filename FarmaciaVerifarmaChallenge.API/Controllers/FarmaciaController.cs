using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.CompilerServices;

namespace FarmaciaVerifarmaChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmaciaController : Controller
    {
        private readonly IFarmaciaService _farmaciaService;
        private readonly ILogger<FarmaciaController> _logger;

        public FarmaciaController(IFarmaciaService farmaciaService, ILogger<FarmaciaController> logger)
        {
            _farmaciaService = farmaciaService;
            _logger = logger;
        }

        /// <summary>
        /// Agregar Farmacia Nueva
        /// </summary>
        /// <remarks>
        /// Ejemplo de cuerpo de solicitud:
        /// 
        ///     {
        ///         "nombre": "Farmacia San Jose",
        ///         "direccion": "Rivadavia 123",
        ///         "latitud": -34.60372,
        ///         "longitud": 60.38159
        ///     }
        /// </remarks>
        /// <param name="farmacia">Datos de farmacia.</param>
        /// <returns>Devuelve 200 si se agrega exitosamente.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Agregar nueva farmacia", Description = "Agregar nueva farmacia en la db")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddFarmacia([FromBody] FarmaciaDto farmacia)
        {
            try
            {
                await _farmaciaService.AddFarmacia(farmacia);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al añadir nueva farmacia");
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Traer Farmacia por Id
        /// </summary>
        /// <param name="id">Farmacia id</param>
        /// <returns>Farmacia dto</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Traer farmacia por ID", Description = "Obtener los datos de una farmacia por ID.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FarmaciaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FarmaciaDto>> GetFarmaciaById(int id)
        {
            try
            {
                var farmacia = await _farmaciaService.GetFarmaciaById(id);
                if (farmacia == null) return NotFound();

                return Ok(farmacia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al traer la farmacia con {Id}", id);
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Traer Farmarcia por cercania segun las coordenadas del paciente
        /// </summary>
        /// <param name="lat">Latitud actual</param>
        /// <param name="lon">Longitud actual</param>
        /// <returns>Farmacia más cercana</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Traer farmacia por cercania",
            Description = "Devuelve la farmacia mas cercana a las coordenadas dadas"
        )]
        [SwaggerResponse(200, "Farmacia cercana encontrada", typeof(FarmaciaDto))]
        [SwaggerResponse(404, "No se encontro ninguna farmacia cercana")]
        [HttpGet]
        public async Task<ActionResult<FarmaciaDto>> GetFarmaciaPorCercania([FromQuery] decimal lat, [FromQuery] decimal lon)
        {
            try
            {
                var farmacia = await _farmaciaService.GetFarmaciaPorCercania(lat, lon);
                if (farmacia == null) return NotFound("No se encontro farmaciacercana");

                return Ok(farmacia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al traer la farmacia cercana");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
