using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmaciaVerifarmaChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmaciaController : Controller
    {
        private readonly IFarmaciaService _farmaciaService;

        public FarmaciaController(IFarmaciaService farmaciaService)
        {
            _farmaciaService = farmaciaService;
        }

        [HttpPost]
        public async Task<ActionResult> AddFarmacia([FromBody] FarmaciaDto farmacia)
        {
            await _farmaciaService.AddFarmacia(farmacia);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FarmaciaDto>> GetFarmaciaById(int id)
        {
            var farmacia = await _farmaciaService.GetFarmaciaById(id);
            return Ok(farmacia);
        }

        [HttpGet]
        public async Task<ActionResult<FarmaciaDto>> GetFarmaciaPorCercania([FromQuery] decimal lat, [FromQuery] decimal lon)
        {
            var farmacia = await _farmaciaService.GetFarmaciaPorCercania(lat, lon);
            return Ok(farmacia);
        }
    }
}
