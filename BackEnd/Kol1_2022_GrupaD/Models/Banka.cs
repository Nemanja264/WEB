namespace Models;

public class Banka
{
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public string Lokacija { get; set; }

    [RegularExpression(@"^0\d\d{8,9}$", ErrorMessage = "Nevalidan format broja telefona")]
    public string Telefon { get; set; }
    public int BrZaposlenih { get; set; }
    public List<Racun>? Racuni { get; set; }
}