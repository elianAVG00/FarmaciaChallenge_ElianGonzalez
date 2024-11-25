using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;
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

        public FarmaciaService(IFarmaciaRepository farmaciaRepository)
        {
            _farmaciaRepository = farmaciaRepository;
        }

        public async Task AddFarmacia(FarmaciaDto farmacia)
        {
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

        public async Task<FarmaciaDto> GetFarmacia(int id)
        {
            var farmacia = await _farmaciaRepository.GetFarmaciaById(id);
            var farmaciaDto = new FarmaciaDto()
            {
                Nombre = farmacia.Nombre,
                Direccion = farmacia.Direccion,
                Latitud = farmacia.Latitud,
                Longitud = farmacia.Longitud
            };

            return farmaciaDto;
        }

        public async Task<FarmaciaDto> GetFarmaciaById(int farmaciaId)
        {
            var farmacia = await _farmaciaRepository.GetFarmaciaById(farmaciaId);
            var farmaciaDto = new FarmaciaDto()
            {
                Nombre = farmacia.Nombre,
                Direccion = farmacia.Direccion,
                Latitud = farmacia.Latitud,
                Longitud = farmacia.Longitud
            };

            return farmaciaDto;
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
            await _farmaciaRepository.UpdateFarmacia(farmacia);
        }

        public async Task<FarmaciaDto> GetFarmaciaPorCercania(decimal latitud, decimal longitud)
        {
            var farmacia = await _farmaciaRepository.GetFarmaciaPorCercania(latitud, longitud);

            var farmaciaDto = new FarmaciaDto()
            {
                Nombre = farmacia.Nombre,
                Direccion = farmacia.Direccion,
                Latitud = farmacia.Latitud,
                Longitud = farmacia.Longitud
            };

            return farmaciaDto;
        }
    }
}
