using FarmaciaVerifarmaChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmaciaVerifarmaChallenge.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmacia> Farmacias { get; set; }
    }
}
