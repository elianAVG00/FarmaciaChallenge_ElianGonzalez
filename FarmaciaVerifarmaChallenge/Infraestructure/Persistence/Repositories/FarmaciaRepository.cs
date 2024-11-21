using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;

namespace FarmaciaVerifarmaChallenge.Infraestructure.Persistence.Repositories
{
    public class FarmaciaRepository: IFarmaciaRepository
    {
        private readonly ApplicationDbContext _db;

        public FarmaciaRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void AddFarmacia(Farmacia Farmacia)
        {
            _db.Farmacias.Add(Farmacia);
            _db.SaveChanges();
        }

        public void DeleteFarmacia(int FarmaciaId)
        {
            var Farmacia = _db.Farmacias.Find(FarmaciaId);
            if (Farmacia == null) return;

            _db.Farmacias.Remove(Farmacia);
            _db.SaveChanges();
        }

        public IEnumerable<Farmacia> GetCategories()
        {
            return _db.Farmacias.ToList();
        }

        public Farmacia GetFarmaciaById(int FarmaciaId)
        {
            return _db.Farmacias.Find(FarmaciaId);
        }

        public void UpdateFarmacia(Farmacia Farmacia)
        {
            var far = _db.Farmacias.Find(Farmacia.Id);
            far.Nombre = Farmacia.Nombre;
            far.Direccion = Farmacia.Direccion;
            far.Longitud = Farmacia.Longitud;
            far.Latitud = Farmacia.Latitud;
            _db.SaveChanges();
        }

    }
}
