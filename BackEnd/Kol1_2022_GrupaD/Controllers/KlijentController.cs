using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class KlijentController : ControllerBase
{
    public BankovniSistemContext Context { get; set; }

    public KlijentController(BankovniSistemContext context)
    {
        Context = context;
    }

    [Route("DodajKlijenta")]
    [HttpPost]
    public async Task<ActionResult> DodajKlijenta([FromBody] KlijentSlanje k)
    {
        try
        {
            var klijent = new Klijent()
            {
                Ime = k.Ime,
                Prezime = k.Prezime,
                Telefon = k.Telefon,
                DatumRodjenja = k.DatumRodjenja
            };

            await Context.clients.AddAsync(klijent);
            await Context.SaveChangesAsync();
            return Ok($"Klijent ID {klijent.ID} dodata");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("DodajRacun")]
    [HttpPost]
    public async Task<ActionResult> OtvoriRacun([FromBody] RacunSlanje r)
    {
        try
        {
            var klijent = await Context.clients.FindAsync(r.KlijentID);
            if (klijent == null)
                return NotFound("Client not found");

            var banka = await Context.banke.FindAsync(r.BankaID);
            if (banka == null)
                return NotFound("Bank not found");

            var racun = new Racun()
            {
                Banka = banka,
                Klijent = klijent,
                BrojRacuna = r.BrojRacuna,
                StanjeRacuna = r.StanjeRacuna,
                UkupnoPodignuto = r.UkupnoPodignuto,
                DatumOtvaranja = r.DatumOtvaranjaRacuna
            };

            if (banka.Racuni == null)
                banka.Racuni = new List<Racun>();

            if (klijent.Racuni == null)
                klijent.Racuni = new List<Racun>();

            banka.Racuni.Add(racun);
            klijent.Racuni!.Add(racun);

            await Context.accounts.AddAsync(racun);
            await Context.SaveChangesAsync();
            return Ok($"Racun ID {racun.ID} otvoren, klijent ID {klijent.ID}, banka ID {banka.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("PromenaStanja")]
    public async Task<ActionResult> PromeniStanje([FromQuery] string BrojRacuna, [FromQuery] double NovoStanje)
    {
        try
        {
            var racun = await Context.accounts
                    .FirstOrDefaultAsync(r => r.BrojRacuna == BrojRacuna);
            if (racun == null)
                return NotFound("Account not found");

            if (racun!.StanjeRacuna > NovoStanje)
                racun.UkupnoPodignuto += racun.StanjeRacuna - NovoStanje;

            racun.StanjeRacuna = NovoStanje;

            Context.accounts.Update(racun);
            await Context.SaveChangesAsync();
            return Ok($"Novo stanje racuna ID {racun.ID} je {racun.StanjeRacuna}, UkupnoPodignuto: {racun.UkupnoPodignuto}");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
