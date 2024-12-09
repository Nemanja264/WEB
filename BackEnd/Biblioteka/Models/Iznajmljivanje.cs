using System.Text.Json.Serialization;

namespace Models;

public class Iznajmljivanje
{
    [Key]
    public int ID { get; set; }
    public DateTime DatumIznajmljivanja { get; set; }
    public DateTime? DatumVracanja { get; set; }
    public int RokVracanja { get; set; }

    [JsonIgnore]
    public Korisnik? Korisnik { get; set; }

    [JsonIgnore]
    public Knjiga? Knjiga { get; set; }

}
