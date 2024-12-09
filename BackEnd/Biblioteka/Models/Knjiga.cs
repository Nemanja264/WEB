namespace Models;

public class Knjiga
{
    [Key]
    public int ID { get; set; }
    public required string Naslov { get; set; }
    public DateTime DatumIzdavanja { get; set; }
    public string Autor { get; set; }
    public string Zanr { get; set; }
    public int BrDostupnih { get; set; }
    public List<Iznajmljivanje>? Iznajmljivanja { get; set; }
}
