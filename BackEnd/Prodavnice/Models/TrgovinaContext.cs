using Models;

namespace WebTemplate.Models;

public class TrgovinaContext : DbContext
{
    // DbSet kolekcije!
    public DbSet<Prodavnica> prodavnice { get; set; }
    public DbSet<Proizvod> proizvodi { get; set; }
    public DbSet<StoreProduct> storeProducts { get; set; }

    public TrgovinaContext(DbContextOptions options) : base(options)
    {

    }
}
