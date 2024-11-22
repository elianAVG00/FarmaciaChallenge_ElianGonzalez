using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;

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

        public async Task<Farmacia> GetFarmacia(int id)
        {
            return await _farmaciaRepository.GetFarmaciaById(id);
        }

        public async Task<Farmacia> GetFarmaciaById(int farmaciaId)
        {
            return await _farmaciaRepository.GetFarmaciaById(farmaciaId);
        }

        public async Task<IEnumerable<Farmacia>> GetFarmacias()
        {
            return await _farmaciaRepository.GetCategories();
        }

        public async Task UpdateFarmacia(Farmacia farmacia)
        {
            throw new NotImplementedException();
        }
    }
}
