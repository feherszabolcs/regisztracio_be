using Microsoft.EntityFrameworkCore;
using regisztracio_be.Models;

namespace regisztracio_be.Context
{
    public class OTDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public OTDbContext(DbContextOptions opt) : base(opt)
        {
            
        }
    }
}
