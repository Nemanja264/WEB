using System.Text.Json.Serialization;

namespace Models;

public class StoreProduct
{
    [Key]
    public int ID { get; set; }
    [JsonIgnore]
    public Prodavnica? Prodavnica { get; set; }
    [JsonIgnore]
    public Proizvod? Proizvod { get; set; }
    public int Cena { get; set; }
    public int Kolicina { get; set; }
}