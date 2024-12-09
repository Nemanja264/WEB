using Models;

namespace WebTemplate.Models;

public class StudijeContext : DbContext
{
    // DbSet kolekcije!
    public DbSet<Fakultet> fakulteti { get; set; }
    public DbSet<Smer> smerovi { get; set; }
    public DbSet<Student> studenti { get; set; }
    public DbSet<Upis> upisi { get; set; }
    public StudijeContext(DbContextOptions options) : base(options)
    {

    }
}
