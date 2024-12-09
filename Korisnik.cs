namespace Models;

public class Korisnik
{
    [Key]
    public int ID { get; set; }
    public required int JKB { get; set; }

    [MaxLength(50)]
    public string Ime { get; set; }

    [MaxLength(50)]
    public string Prezime { get; set; }
    public DateTime datumRodjenja { get; set; }
    public char Pol { get; set; }
    public int brKnjiga { get; set; }
    public List<Iznajmljivanje>? Iznajmljivanja { get; set; }
}