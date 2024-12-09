using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class UstanovaController : ControllerBase
{
    public FirmeContext Context { get; set; }

    public UstanovaController(FirmeContext context)
    {
        Context = context;
    }

    [Route("DodajUstanovu")]
    [HttpPost]
    public async Task<ActionResult> DodajUstanovu([FromBody] Ustanova ustanova)
    {
        try
        {

            await Context.ustanove.AddAsync(ustanova);
            await Context.SaveChangesAsync();
            return Ok("Ustanova dodata");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("DodajUgovor")]
    [HttpPost]
    public async Task<ActionResult> SklopiUgovor([FromQuery] int UstanovaId, [FromQuery] int ZaposlenID, [FromQuery] DateTime datumPotpisa, [FromQuery] int BrojUgovora)
    {
        try
        {
            var ustanova = await Context.ustanove.FindAsync(UstanovaId);
            var zaposlen = await Context.zaposleni.FindAsync(ZaposlenID);

            if (ustanova == null)
                return NotFound("Ustanova nije nadjena");

            if (zaposlen == null)
                return NotFound("Zaposleni nije nadjen");

            var ugovorBaza = new Ugovor()
            {
                BrojUgovora = BrojUgovora,
                datumPotpisivanja = datumPotpisa,
                Zaposlen = zaposlen,
                Ustanova = ustanova
            };

            if (zaposlen.Ugovori == null)
                zaposlen.Ugovori = new List<Ugovor>();

            if (ustanova.Ugovori == null)
                ustanova.Ugovori = new List<Ugovor>();

            zaposlen.Ugovori.Add(ugovorBaza);
            ustanova.Ugovori.Add(ugovorBaza);

            await Context.ugovori.AddAsync(ugovorBaza);
            await Context.SaveChangesAsync();
            return Ok("Ugovor dodat");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("IzmeniUgovor")]
    [HttpPut]
    public async Task<ActionResult> IzmeniUgovor([FromQuery] int ZaposleniId, [FromQuery] int UstanovaId, [FromBody] Ugovor ugovorDTO)
    {
        try
        {
            var ugovor = await Context.ugovori
                .FirstOrDefaultAsync(z => z.Zaposlen.ID == ZaposleniId && z.Ustanova.ID == UstanovaId);

            if (ugovor == null)
                return NotFound("Ugovor nije nadjen");

            ugovor.BrojUgovora = ugovorDTO.BrojUgovora;
            ugovor.datumPotpisivanja = ugovorDTO.datumPotpisivanja;

            await Context.SaveChangesAsync();
            return Ok("Ugovor Updated");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("BrojRadnihDana")]
    [HttpGet]
    public async Task<ActionResult> IzracunajRadneDane([FromQuery] int UstanovaId)
    {
        try
        {
            var ustanova = await Context.ustanove
                .Include(u => u.Ugovori)
                .ThenInclude(z => z.Zaposlen)
                .FirstOrDefaultAsync(ust => ust.ID == UstanovaId);

            if (ustanova == null)
                return NotFound("Ustanova nije nadjena");

            var brDana = ustanova.Ugovori.Sum(z => (DateTime.Now - z.datumPotpisivanja).Days);

            return Ok($"Radnici {ustanova.Naziv} su ukupno proveli radeci ovde {brDana}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
