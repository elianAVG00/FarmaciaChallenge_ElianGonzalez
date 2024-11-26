using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaVerifarmaChallenge.Application.Services
{
    public class FarmaciaService : IFarmaciaService
    {
        private readonly IFarmaciaRepository _farmaciaRepository;
        private readonly ILogger<IFarmaciaService> _logger;
        public FarmaciaService(IFarmaciaRepository farmaciaRepository, ILogger<IFarmaciaService> logger)
        {
            _farmaciaRepository = farmaciaRepository ?? throw new ArgumentNullException(nameof(farmaciaRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddFarmacia(FarmaciaDto farmacia)
        {
            if (farmacia == null) throw new ArgumentNullException(nameof(farmacia));
            ValidarCordenadas(farmacia.Latitud, farmacia.Longitud);

            var farmaciaEntity = new Farmacia()
            {
                Nombre = farmacia.Nombre,
                Direccion = farmacia.Direccion,
                Latitud = farmacia.Latitud,
                Longitud = farmacia.Longitud
            };
            await _farmaciaRepository.AddFarmacia(farmaciaEntity);
        }

        public async Task DeleteFarmacia(int farmaciaId)
        {
            await _farmaciaRepository.DeleteFarmacia(farmaciaId);
        }

        public async Task<FarmaciaDto> GetFarmaciaById(int farmaciaId)
        {
            try
            {
                var farmacia = await _farmaciaRepository.GetFarmaciaById(farmaciaId);
                if (farmacia == null)
                {
                    _logger.LogWarning("No existe la farmacia con id {FarmaciaId}", farmaciaId);
                    return null;
                }
                var farmaciaDto = new FarmaciaDto { Nombre = farmacia.Nombre, Direccion = farmacia.Direccion, 
                    Latitud = farmacia.Latitud, Longitud = farmacia.Longitud };
                _logger.LogInformation("Farmacia encontrada {FarmaciaDto}", farmaciaDto); return farmaciaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la farmacia con ID: {FarmaciaId}", farmaciaId); 
                throw;
            }

        }

        public async Task<IEnumerable<FarmaciaDto>> GetFarmacias()
        {
            var farmacias = await _farmaciaRepository.GetCategories();

            var farmaciasDto = farmacias.Select(farmacia => new FarmaciaDto
            {
                Nombre = farmacia.Nombre,
                Direccion = farmacia.Direccion,
                Latitud = farmacia.Latitud,
                Longitud = farmacia.Longitud
            });

            return farmaciasDto;
        }

        public async Task UpdateFarmacia(Farmacia farmacia)
        {
            if (farmacia == null) throw new ArgumentNullException(nameof(farmacia));
            ValidarCordenadas(farmacia.Latitud, farmacia.Longitud);

            await _farmaciaRepository.UpdateFarmacia(farmacia);
        }

        public async Task<FarmaciaDto> GetFarmaciaPorCercania(decimal latitud, decimal longitud)
        {
            try
            {
                var farmacia = await _farmaciaRepository.GetFarmaciaPorCercania(latitud, longitud);
                if (farmacia == null)
                {
                    _logger.LogWarning("No se ha encontrado farmacia cercana");
                    return null;
                }
                var farmaciaDto = new FarmaciaDto
                {
                    Nombre = farmacia.Nombre,
                    Direccion = farmacia.Direccion,
                    Latitud = farmacia.Latitud,
                    Longitud = farmacia.Longitud
                };
                _logger.LogInformation("Farmacia encontrada {FarmaciaDto}", farmaciaDto); return farmaciaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar farmacia mas cercana"); 
                throw;
            }
        }

        private void ValidarCordenadas(decimal latitud, decimal longitud)
        {
            if (latitud < -90 || latitud > 90)
                throw new ArgumentOutOfRangeException(nameof(latitud), "La latitud debe estar entre -90 y 90 grados");
            if (longitud < -180 || longitud > 180)
                throw new ArgumentOutOfRangeException(nameof(longitud), "La longitud debe estar entre -180 y 180 grado");
        }
    }
}
