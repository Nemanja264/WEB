namespace Models;

public class Klijent
{
    [Key]
    public int ID { get; set; }

    [MaxLength(20)]
    public required string Ime { get; set; }

    [MaxLength(20)]
    public required string Prezime { get; set; }
    public DateTime DatumRodjenja { get; set; }

    [RegularExpression(@"^0\d\d{8,9}$", ErrorMessage = "Nevalidan format broja telefona")]
    public string Telefon { get; set; }
    public List<Racun>? Racuni { get; set; }
}