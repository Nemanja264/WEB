using System.Text.Json.Serialization;

namespace Models;

public class Ugovor
{
    [Key]
    public int ID { get; set; }
    public required int BrojUgovora { get; set; }
    public DateTime datumPotpisivanja { get; set; }

    [JsonIgnore]
    public Ustanova? Ustanova { get; set; }

    [JsonIgnore]
    public Zaposleni? Zaposlen { get; set; }
}