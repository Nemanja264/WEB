namespace Models;

public class Restoran
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }
    public int maxGostiju { get; set; }
    public int BrRadnihMesta { get; set; }

    [RegularExpression(@"^0\d\d{8,9}$", ErrorMessage = "Nevalidan format broja")]
    public string? Telefon { get; set; }
    public List<Zaposlenje>? Zaposleni { get; set; }
}