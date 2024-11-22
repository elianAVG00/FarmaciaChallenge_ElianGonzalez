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

        public async Task AddFarmacia(Farmacia Farmacia)
        {
            await _db.Farmacias.AddAsync(Farmacia);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFarmacia(int FarmaciaId)
        {
            var Farmacia = await _db.Farmacias.FindAsync(FarmaciaId);
            if (Farmacia == null) return;

            _db.Farmacias.Remove(Farmacia); 
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Farmacia>> GetCategories()
        {
            return await _db.Farmacias.ToListAsync();
        }

        public async Task<Farmacia> GetFarmaciaById(int FarmaciaId)
        {
            return await _db.Farmacias.FindAsync(FarmaciaId);
        }

        public async Task UpdateFarmacia(Farmacia Farmacia)
        {
            var far = await _db.Farmacias.FindAsync(Farmacia.Id);
            far.Nombre = Farmacia.Nombre;
            far.Direccion = Farmacia.Direccion;
            far.Longitud = Farmacia.Longitud;
            far.Latitud = Farmacia.Latitud;
            _db.SaveChanges();
        }

    }
}
