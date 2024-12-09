using System.Text.Json.Serialization;

namespace Models;

public class Smer
{
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }
    [JsonIgnore]
    public Fakultet? Fakultet { get; set; }
}