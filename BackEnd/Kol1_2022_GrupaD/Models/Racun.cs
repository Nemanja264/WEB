using System.Text.Json.Serialization;

namespace Models;

public class Racun
{
    [Key]
    public int ID { get; set; }

    [RegularExpression(@"^\d{10}$", ErrorMessage = "Nevalidan format za broj racuna")]
    public required string BrojRacuna { get; set; }
    public DateTime DatumOtvaranja { get; set; }
    public double StanjeRacuna { get; set; }
    public double UkupnoPodignuto { get; set; }

    [JsonIgnore]
    public Klijent? Klijent { get; set; }

    [JsonIgnore]
    public Banka? Banka { get; set; }
}