using FarmaciaVerifarmaChallenge.Domain.Entities;

namespace FarmaciaVerifarmaChallenge.Application.Interfaces
{
    public interface IFarmaciaService
    {
        public void AddFarmacia(Farmacia farmacia);

        public Farmacia GetFarmaciaById(int farmaciaId);

        public void DeleteFarmacia(int farmaciaId);

        public void UpdateFarmacia(Farmacia farmacia);

        public IEnumerable<Farmacia> GetFarmacias();

    }
}
