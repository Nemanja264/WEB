namespace Models;

public class Kuvar
{
    [Key]
    public int ID { get; set; }

    [MaxLength(20)]
    public required string Ime { get; set; }

    [MaxLength(20)]
    public required string Prezime { get; set; }
    public DateTime DatumRodjenja { get; set; }
    public string? StrucnaSprema { get; set; }
    public List<Zaposlenje>? Zaposlenja { get; set; }

}