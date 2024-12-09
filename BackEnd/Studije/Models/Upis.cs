using System.Text.Json.Serialization;

namespace Models;

public class Upis
{
    [Key]
    public int ID { get; set; }
    public DateTime DatumUpisa { get; set; }
    public int ESPB { get; set; }

    [JsonIgnore]
    public Student? Student { get; set; }

    [JsonIgnore]
    public Smer? Smer { get; set; }

    [JsonIgnore]
    public Fakultet? Fakultet { get; set; }
}