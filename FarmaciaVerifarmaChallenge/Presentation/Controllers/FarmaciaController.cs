using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmaciaVerifarmaChallenge.Presentation.Controllers
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
        public async Task<ActionResult<Farmacia>> GetFarmaciaById(int id)
        {
            var farmacia = await _farmaciaService.GetFarmaciaById(id);
            return Ok(farmacia);
        }
    }
}
