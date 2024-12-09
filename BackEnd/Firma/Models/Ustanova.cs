namespace Models;

public class Ustanova
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }
    public string Adresa { get; set; }

    [Length(10, 10)]
    public string BrTelefona { get; set; }
    public string Email { get; set; }
    public List<Ugovor>? Ugovori { get; set; }

}