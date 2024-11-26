using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
        [HttpPost]
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
        [HttpGet("{id}")]
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
        /// Traer Farmarcia por cercania segun su coordenadas
        /// </summary>
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
