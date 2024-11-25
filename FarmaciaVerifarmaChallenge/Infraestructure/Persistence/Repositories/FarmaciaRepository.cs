using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmaciaVerifarmaChallenge.Infraestructure.Persistence.Repositories
{
    public class FarmaciaRepository: IFarmaciaRepository
    {
        private readonly ApplicationDbContext _db;

        public FarmaciaRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task AddFarmacia(Farmacia farmacia)
        {
            await _db.Farmacias.AddAsync(farmacia);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFarmacia(int farmaciaId)
        {
            var Farmacia = await _db.Farmacias.FindAsync(farmaciaId);
            if (Farmacia == null) return;

            _db.Farmacias.Remove(Farmacia); 
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Farmacia>> GetCategories()
        {
            return await _db.Farmacias.ToListAsync();
        }

        public async Task<Farmacia> GetFarmaciaById(int farmaciaId)
        {
            return await _db.Farmacias.FindAsync(farmaciaId);
        }

        public async Task UpdateFarmacia(Farmacia farmacia)
        {
            var far = await _db.Farmacias.FindAsync(farmacia.Id);
            far.Nombre = farmacia.Nombre;
            far.Direccion = farmacia.Direccion;
            far.Longitud = farmacia.Longitud;
            far.Latitud = farmacia.Latitud;
            _db.SaveChanges();
        }

        public Task<Farmacia> GetFarmaciaPorCercania(decimal latitud, decimal longitud)
        {
            return _db.Farmacias.OrderBy(f => CalculoDistancia(latitud, longitud, f.Latitud, f.Longitud))
               .FirstOrDefaultAsync();
        }

        private static double CalculoDistancia(decimal latitudPaciente, decimal longitudPaciente, decimal latitudFarmacia, decimal longitudFarmacia)
        {
            decimal diferenciaLatitud = latitudFarmacia - latitudPaciente;
            double diferenciaLongitud = (double)(longitudFarmacia - longitudPaciente) * 
                Math.Cos((double)(latitudPaciente + latitudFarmacia) / 2 * Math.PI / 180);

            return Math.Sqrt(Math.Pow((double)diferenciaLatitud, 2) + Math.Pow(diferenciaLongitud, 2));
        }

    }
}
