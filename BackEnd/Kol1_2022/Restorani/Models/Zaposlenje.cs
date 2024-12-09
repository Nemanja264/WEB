using System.Text.Json.Serialization;

namespace Models;

public class Zaposlenje
{
    [Key]
    public int ID { get; set; }
    public DateTime DatumZaposlenja { get; set; }
    public int Plata { get; set; }

    [MaxLength(20)]
    public required string Pozicija { get; set; }

    [JsonIgnore]
    public Restoran? Restoran { get; set; }

    [JsonIgnore]
    public Kuvar? Kuvar { get; set; }
}