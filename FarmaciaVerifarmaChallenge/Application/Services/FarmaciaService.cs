using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;

namespace FarmaciaVerifarmaChallenge.Application.Services
{
    public class FarmaciaService : IFarmaciaService
    {
        private readonly IFarmaciaRepository _farmaciaRepository;

        public FarmaciaService(IFarmaciaRepository farmaciaRepository)
        {
            this._farmaciaRepository = farmaciaRepository;
        }

        public void AddFarmacia(Farmacia farmacia)
        {
            throw new NotImplementedException();
        }

        public void DeleteFarmacia(int farmaciaId)
        {
            throw new NotImplementedException();
        }

        public Farmacia GetFarmacia(int id)
        {
            throw new NotImplementedException();
        }

        public Farmacia GetFarmaciaById(int farmaciaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Farmacia> GetFarmacias()
        {
            throw new NotImplementedException();
        }

        public void UpdateFarmacia(Farmacia farmacia)
        {
            throw new NotImplementedException();
        }
    }
}
