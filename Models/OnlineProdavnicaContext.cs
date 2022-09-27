using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class OnlineProdavnicaContext : DbContext
    {
        public DbSet<Radnja> Radnje { get; set; }
        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<KorpaSpoj> KorpaSpojevi { get; set; }
        public DbSet<RacunSpoj> RacunSpojevi { get; set; }

        public OnlineProdavnicaContext(DbContextOptions options) : base(options)
        {

        }
    }
}