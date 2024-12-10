using Models;

namespace WebTemplate.Models;

public class BankovniSistemContext : DbContext
{
    // DbSet kolekcije!
    public DbSet<Banka> banke { get; set; }
    public DbSet<Klijent> clients { get; set; }
    public DbSet<Racun> accounts { get; set; }
    public BankovniSistemContext(DbContextOptions options) : base(options)
    {

    }
}
