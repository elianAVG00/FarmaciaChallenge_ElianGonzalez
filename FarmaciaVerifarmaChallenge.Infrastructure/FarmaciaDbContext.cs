using FarmaciaVerifarmaChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaVerifarmaChallenge.Infrastructure
{
    public class FarmaciaDbContext : DbContext
    {
        public FarmaciaDbContext(DbContextOptions<FarmaciaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmacia> Farmacias { get; set; }
    }
}
