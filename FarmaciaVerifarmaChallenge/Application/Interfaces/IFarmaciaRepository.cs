using FarmaciaVerifarmaChallenge.Domain.Entities;

namespace FarmaciaVerifarmaChallenge.Application.Interfaces
{
    public interface IFarmaciaRepository
    {
        Task<IEnumerable<Farmacia>> GetCategories();
        Task AddFarmacia(Farmacia Farmacia);
        Task UpdateFarmacia(Farmacia Farmacia);
        Task<Farmacia> GetFarmaciaById(int FarmaciaId);
        Task DeleteFarmacia(int FarmaciaId);
    }
}
