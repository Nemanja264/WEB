namespace Models;

public class Zaposleni
{
    [Key]
    public int ID { get; set; }

    [Length(13, 13)]
    public required string JMBG { get; set; }

    [MaxLength(50)]
    public required string Ime { get; set; }

    [MaxLength(50)]
    public required string Prezime { get; set; }
    public DateTime DatumRodjenja { get; set; }

    [StringLength(10, MinimumLength = 7)]
    public string? BrTelefona { get; set; }
    public List<Ugovor>? Ugovori { get; set; }

}