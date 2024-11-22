using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Domain.Entities;

namespace FarmaciaVerifarmaChallenge.Application.Interfaces
{
    public interface IFarmaciaService
    {
        Task AddFarmacia(FarmaciaDto farmacia);

        Task<Farmacia> GetFarmaciaById(int farmaciaId);

        Task DeleteFarmacia(int farmaciaId);

        Task UpdateFarmacia(Farmacia farmacia);

        Task<IEnumerable<Farmacia>> GetFarmacias();

    }
}
