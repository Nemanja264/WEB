namespace Models;

public class Fakultet
{
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public string? Adresa { get; set; }
    public string? Telefon { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
    public List<Smer>? Smerovi { get; set; }
    public List<Student>? Studenti { get; set; }
    public List<Upis>? Upisi { get; set; }
}