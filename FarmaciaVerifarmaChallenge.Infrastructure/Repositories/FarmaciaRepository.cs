using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmaciaVerifarmaChallenge.Infrastructure.Repositories
{
    public class FarmaciaRepository : IFarmaciaRepository
    {
        private readonly FarmaciaDbContext _db;
        private readonly ILogger _logger;

        public FarmaciaRepository(FarmaciaDbContext db, ILogger logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddFarmacia(Farmacia farmacia)
        {
            if (farmacia == null) throw new ArgumentNullException(nameof(farmacia));

            try
            {
                await _db.Farmacias.AddAsync(farmacia);
                await _db.SaveChangesAsync();
                _logger.Information("Farmacia agregada con éxito: {@farmacia}", farmacia);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al agregar la farmacia: {@farmacia}", farmacia);
                throw;
            }
        }

        public async Task DeleteFarmacia(int farmaciaId)
        {
            try
            {
                var farmacia = await _db.Farmacias.FindAsync(farmaciaId);
                if (farmacia == null)
                {
                    _logger.Warning("No existe la farmaca {farmaciaId}.", farmaciaId);
                    return;
                }

                _db.Farmacias.Remove(farmacia);
                await _db.SaveChangesAsync();
                _logger.Information("Farmacia eliminada con exito: {@farmacia}", farmacia);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar la farmacia {farmaciaId}", farmaciaId);
                throw;
            }
        }

        public async Task<IEnumerable<Farmacia>> GetCategories()
        {
            try
            {
                return await _db.Farmacias.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al traerse todas las farmacias");
                throw;
            }
        }

        public async Task<Farmacia> GetFarmaciaById(int farmaciaId)
        {
            try
            {
                var farmacia = await _db.Farmacias.FindAsync(farmaciaId);
                if (farmacia == null)
                {
                    _logger.Warning("No existe la farmacia {farmaciaId}", farmaciaId);
                }
                return farmacia;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al trae la farmacia {farmaciaId}", farmaciaId);
                throw;
            }
        }

        public async Task UpdateFarmacia(Farmacia farmacia)
        {
            if (farmacia == null) throw new ArgumentNullException(nameof(farmacia));

            try
            {
                var farmaciaDb = await _db.Farmacias.FindAsync(farmacia.Id);
                if (farmaciaDb == null)
                {
                    _logger.Warning("No existe la farmacia {farmaciaId}", farmacia.Id);
                    return;
                }

                farmaciaDb.Nombre = farmacia.Nombre;
                farmaciaDb.Direccion = farmacia.Direccion;
                farmaciaDb.Latitud = farmacia.Latitud;
                farmaciaDb.Longitud = farmacia.Longitud;

                await _db.SaveChangesAsync();
                _logger.Information("Farmacia actualizada {@farmacia}", farmacia);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar la farmacia {@farmacia}", farmacia);
                throw;
            }
        }

        public async Task<Farmacia> GetFarmaciaPorCercania(decimal latitud, decimal longitud)
        {
            try
            {
                var farmacias = await _db.Farmacias.ToListAsync();

                if (!farmacias.Any())
                {
                    _logger.Warning("No se encontraron farmacias en la base de datos.");
                    return null;
                }

                var farmaciaMasCercana = farmacias.Select(f => new { Farmacia = f, Distancia = CalculoDistancia(latitud, longitud, f.Latitud, f.Longitud)})
                                                            .OrderBy(f => f.Distancia).FirstOrDefault();

                if (farmaciaMasCercana == null)
                {
                    _logger.Warning("Error al traer la farmacias mas cercana");
                    return null;
                }

                return farmaciaMasCercana.Farmacia;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al traer la farmacias mas cercana");
                throw;
            }
        }

        private static double CalculoDistancia(decimal latitudPaciente, decimal longitudPaciente, decimal latitudFarmacia, decimal longitudFarmacia)
        {
            try
            {
                decimal diferenciaLatitud = latitudFarmacia - latitudPaciente;
                double diferenciaLongitud = (double)(longitudFarmacia - longitudPaciente) *
                    Math.Cos((double)(latitudPaciente + latitudFarmacia) / 2 * Math.PI / 180);

                return Math.Sqrt(Math.Pow((double)diferenciaLatitud, 2) + Math.Pow(diferenciaLongitud, 2));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al calcular la distancia entre las coordenadass", ex);
            }
        }

    }
}
