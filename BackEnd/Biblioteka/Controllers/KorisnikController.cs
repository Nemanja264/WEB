using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{
    public BibliotekaContext Context { get; set; }

    public KorisnikController(BibliotekaContext context)
    {
        Context = context;
    }

    [Route("DodajKorisnika")]
    [HttpPost]
    public async Task<ActionResult> DodajKorisnika([FromBody] Korisnik korisnik)
    {
        try
        {
            await Context.korisnici.AddAsync(korisnik);
            await Context.SaveChangesAsync();
            return Ok($"Korisnik sa ID {korisnik.ID} je dodat");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("VracanjeKnjige")]
    [HttpPut]
    public async Task<ActionResult> VratiKnjigu([FromQuery] int KorisnikID, [FromQuery] int KnjigaID)
    {
        try
        {
            var iznajmljivanje = await Context.rents
                    .FirstOrDefaultAsync(i => i.Knjiga.ID == KnjigaID && i.Korisnik.ID == KorisnikID);

            if (iznajmljivanje == null)
                return NotFound("Korisnik nije iznajmio datu knjigu");

            iznajmljivanje.DatumVracanja = DateTime.Now;

            var knjiga = iznajmljivanje.Knjiga;
            var korisnik = iznajmljivanje.Korisnik;

            knjiga.BrDostupnih++;
            korisnik.brKnjiga--;

            await Context.SaveChangesAsync();
            return Ok("Knjiga je vracena");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
