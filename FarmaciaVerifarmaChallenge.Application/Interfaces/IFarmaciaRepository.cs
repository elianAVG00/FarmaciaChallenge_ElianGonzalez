using FarmaciaVerifarmaChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaVerifarmaChallenge.Application.Interfaces
{
    public interface IFarmaciaRepository
    {
        Task<IEnumerable<Farmacia>> GetCategories();

        Task AddFarmacia(Farmacia farmacia);

        Task UpdateFarmacia(Farmacia farmacia);

        Task<Farmacia> GetFarmaciaById(int farmaciaId);

        Task DeleteFarmacia(int farmaciaId);

        Task<Farmacia> GetFarmaciaPorCercania(decimal latitud, decimal longitud);
    }
}
