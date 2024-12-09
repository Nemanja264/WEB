using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class RestoranController : ControllerBase
{
    public RestoranContext Context { get; set; }

    public RestoranController(RestoranContext context)
    {
        Context = context;
    }

    [Route("DodajRestoran")]
    [HttpPost]
    public async Task<ActionResult> DodajRestoran([FromBody] RestoranSlanje r)
    {
        try
        {
            var restoran = new Restoran()
            {
                Naziv = r.Naziv,
                Telefon = r.Telefon,
                maxGostiju = r.maxGostiju,
                BrRadnihMesta = r.maxKuvara
            };

            await Context.restorani.AddAsync(restoran);
            await Context.SaveChangesAsync();
            return Ok($"Restoran sa ID {restoran.ID} je dodat");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("ZaposliKuvara")]
    [HttpPost]
    public async Task<ActionResult> ZaposliKuvara([FromBody] ZaposliSlanje z)
    {
        try
        {
            var restoran = await Context.restorani.FindAsync(z.RestoranID);
            if (restoran == null)
                return NotFound("Restoran not found");

            if (restoran.BrRadnihMesta == 0)
                return BadRequest("Sva radna mesta su popunjena");

            var kuvar = await Context.kuvari.FindAsync(z.KuvarID);
            if (kuvar == null)
                return NotFound("Kuvar not found");

            var zaposlenje = new Zaposlenje()
            {
                Restoran = restoran,
                Kuvar = kuvar,
                Plata = z.Plata,
                Pozicija = z.Pozicija,
                DatumZaposlenja = z.DatumZaposlenja
            };

            if (kuvar.Zaposlenja == null)
                kuvar.Zaposlenja = new List<Zaposlenje>();

            kuvar.Zaposlenja.Add(zaposlenje);

            if (restoran.Zaposleni == null)
                restoran.Zaposleni = new List<Zaposlenje>();

            restoran.Zaposleni.Add(zaposlenje);
            restoran.BrRadnihMesta--;

            await Context.zaposlenja.AddAsync(zaposlenje);
            await Context.SaveChangesAsync();
            return Ok($"Zaposlenje ID {zaposlenje.ID} je dodato");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("OtpustiKuvara")]
    [HttpDelete]
    public async Task<ActionResult> OtpustiKuvara([FromQuery] int KuvarID, [FromQuery] int RestoranID)
    {
        try
        {
            var restoran = await Context.restorani.FindAsync(RestoranID);
            if (restoran == null)
                return NotFound("Restoran not found");

            var kuvar = await Context.kuvari.FindAsync(KuvarID);
            if (kuvar == null)
                return NotFound("Kuvar not found");

            var zaposlenje = await Context.zaposlenja
                    .FirstOrDefaultAsync(k => k.Restoran.ID == RestoranID && k.Kuvar.ID == KuvarID);

            if (zaposlenje == null)
                return NotFound($"Kuvar sa ID {KuvarID} ne radi u restoranu {restoran.Naziv}");

            restoran.Zaposleni.Remove(zaposlenje);
            restoran.BrRadnihMesta++;
            kuvar.Zaposlenja.Remove(zaposlenje);

            Context.zaposlenja.Remove(zaposlenje);
            await Context.SaveChangesAsync();
            return Ok($"Kuvar {kuvar.Ime} sa ID {KuvarID} je otpusten iz restorana {restoran.Naziv} ID {RestoranID}");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("UkupnaPlataKuvara")]
    [HttpGet]
    public async Task<ActionResult> UkupnaPlata([FromQuery] int KuvarID)
    {
        try
        {
            var kuvar = await Context.kuvari
             .Include(k => k.Zaposlenja)
                .FirstOrDefaultAsync(k => k.ID == KuvarID);

            if (kuvar == null)
                return NotFound("Kuvar not found");

            if (kuvar.Zaposlenja == null || !kuvar.Zaposlenja.Any())
                return BadRequest("Kuvar nema zaposlenja");

            var UkupnaZarada = kuvar.Zaposlenja.Sum(p => p.Plata);
            return Ok($"Ukupna zarada kuvara ID {kuvar.ID} je {UkupnaZarada}");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("KuvarSaNajviseZaposljenja")]
    [HttpGet]
    public async Task<ActionResult> NadjiKuvara()
    {
        try
        {
            var kuvar = await Context.kuvari
                .Include(z => z.Zaposlenja)
                .OrderByDescending(k => k.Zaposlenja.Count)
                .Select(k => new
                {
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    DatumRodjenja = k.DatumRodjenja,
                    StrucnaSprema = k.StrucnaSprema,
                    BrojZaposlenja = k.Zaposlenja.Count
                })
                .FirstOrDefaultAsync();

            return Ok(kuvar);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
