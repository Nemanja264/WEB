using Models;

namespace WebTemplate.Models;

public class RestoranContext : DbContext
{
    // DbSet kolekcije!
    public DbSet<Kuvar> kuvari { get; set; }
    public DbSet<Restoran> restorani { get; set; }
    public DbSet<Zaposlenje> zaposlenja { get; set; }
    public RestoranContext(DbContextOptions options) : base(options)
    {

    }
}
