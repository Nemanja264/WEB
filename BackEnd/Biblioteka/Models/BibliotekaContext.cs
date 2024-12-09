using Models;

namespace WebTemplate.Models;

public class BibliotekaContext : DbContext
{
    // DbSet kolekcije!
    public DbSet<Korisnik> korisnici { get; set; }
    public DbSet<Knjiga> knjige { get; set; }
    public DbSet<Iznajmljivanje> rents { get; set; }
    public BibliotekaContext(DbContextOptions options) : base(options)
    {

    }
}
