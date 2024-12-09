namespace Models;

public class Prodavnica
{
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public required string Adresa { get; set; }

    [StringLength(10, MinimumLength = 10)]
    [RegularExpression(@"^\d+$")]
    public string BrojTelefona { get; set; }
    public string Menadzer { get; set; }
    public List<StoreProduct>? ProdProiz { get; set; }
}