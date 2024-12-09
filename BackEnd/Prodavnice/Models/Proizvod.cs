namespace Models;

public class Proizvod
{
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }

    [StringLength(13, MinimumLength = 13)]
    [RegularExpression(@"^\d+$")]
    public string Identifikator { get; set; }
    public required DateTime DatumProizvodnje { get; set; }
    public required DateTime DatumIstekaRoka { get; set; }
    public List<StoreProduct>? ProizProd { get; set; }
}