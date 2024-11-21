using FarmaciaVerifarmaChallenge.Domain.Entities;

namespace FarmaciaVerifarmaChallenge.Application.Interfaces
{
    public interface IFarmaciaRepository
    {
        IEnumerable<Farmacia> GetCategories();
        void AddFarmacia(Farmacia Farmacia);
        void UpdateFarmacia(Farmacia Farmacia);
        Farmacia GetFarmaciaById(int FarmaciaId);
        void DeleteFarmacia(int FarmaciaId);
    }
}
