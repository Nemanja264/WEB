using System.Text.Json.Serialization;

namespace Models;

public class Student
{
    [Key]
    public int ID { get; set; }
    public required int brIndexa { get; set; }

    [MaxLength(50)]
    public string? Ime { get; set; }

    [MaxLength(50)]
    public string? Prezime { get; set; }
    public DateTime DatumRodjenja { get; set; }
    public string? ZavrsenaSrednja { get; set; }

    [JsonIgnore]
    public Fakultet? Fakultet { get; set; }

    [JsonIgnore]
    public Smer? Smer { get; set; }
}