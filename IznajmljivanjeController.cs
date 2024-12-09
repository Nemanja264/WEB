using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IznajmljivanjeController : ControllerBase
{
    public BibliotekaContext Context { get; set; }

    public IznajmljivanjeController(BibliotekaContext context)
    {
        Context = context;
    }

    [Route("IznajmiKnjigu")]
    [HttpPost]
    public async Task<ActionResult> Iznajmljivanje([FromQuery] int KorisnikID, [FromQuery] int KnjigaID, [FromQuery] int BrDana)
    {
        try
        {
            var korisnik = await Context.korisnici.FirstOrDefaultAsync(k => k.ID == KorisnikID);
            var knjiga = await Context.knjige.FirstOrDefaultAsync(k => k.ID == KnjigaID);

            if (korisnik == null)
                return NotFound("Korisnik nije nadjen");
            if (knjiga == null)
                return NotFound("Knjiga nije nadjena");

            if (knjiga.BrDostupnih == 0)
                return NotFound($"Nema dostupnih primeraka {knjiga.Naslov}");

            if (korisnik.brKnjiga > 5)
                return BadRequest($"Korisnik {korisnik.JKB} je iznajmio vise od 5 knjiga");

            var iznajmljivanje = new Iznajmljivanje()
            {
                DatumIznajmljivanja = DateTime.Now,
                RokVracanja = BrDana,
                Korisnik = korisnik,
                Knjiga = knjiga
            };

            knjiga.BrDostupnih--;
            korisnik.brKnjiga++;

            if (knjiga.Iznajmljivanja == null)
                knjiga.Iznajmljivanja = new List<Iznajmljivanje>();

            if (korisnik.Iznajmljivanja == null)
                korisnik.Iznajmljivanja = new List<Iznajmljivanje>();

            knjiga.Iznajmljivanja.Add(iznajmljivanje);
            korisnik.Iznajmljivanja.Add(iznajmljivanje);

            await Context.rents.AddAsync(iznajmljivanje);
            await Context.SaveChangesAsync();
            return Ok($"Knjiga {knjiga.Naslov} je izdata korisniku {korisnik.JKB}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("NadjiIstekleRokove")]
    [HttpGet]
    public async Task<ActionResult> NadjiKorisnike()
    {
        try
        {
            var korisnici = await Context.rents
                    .Include(k => k.Korisnik)
                    .Where(k => k.DatumVracanja != null ?
                            k.DatumIznajmljivanja.AddDays(k.RokVracanja) < k.DatumVracanja
                            : k.DatumIznajmljivanja.AddDays(k.RokVracanja) < DateTime.Now)
                    .Select(k => new
                    {
                        k.Korisnik.JKB,
                        k.Knjiga.Naslov,
                        k.DatumIznajmljivanja,
                        DueDate = k.DatumIznajmljivanja.AddDays(k.RokVracanja),
                        k.DatumVracanja
                    })
                    .ToListAsync();

            if (korisnici == null)
                return NotFound("Svi korisnici su vracali na vreme svoje knjige");

            return Ok(korisnici);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
