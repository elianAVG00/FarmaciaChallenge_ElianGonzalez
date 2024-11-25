using FarmaciaVerifarmaChallenge.Application.Dtos;
using FarmaciaVerifarmaChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaVerifarmaChallenge.Application.Interfaces
{
    public interface IFarmaciaService
    {
        Task AddFarmacia(FarmaciaDto farmacia);

        Task<FarmaciaDto> GetFarmaciaById(int farmaciaId);

        Task DeleteFarmacia(int farmaciaId);

        Task UpdateFarmacia(Farmacia farmacia);

        Task<IEnumerable<FarmaciaDto>> GetFarmacias();

        Task<FarmaciaDto> GetFarmaciaPorCercania(decimal latitud, decimal longitud);

    }
}
