using Models;

namespace WebTemplate.Models;

public class FirmeContext : DbContext
{
    // DbSet kolekcije!
    public DbSet<Zaposleni> zaposleni { get; set; }
    public DbSet<Ustanova> ustanove { get; set; }
    public DbSet<Ugovor> ugovori { get; set; }

    public FirmeContext(DbContextOptions options) : base(options)
    {

    }
}
