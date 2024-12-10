using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class BankaController : ControllerBase
{
    public BankovniSistemContext Context { get; set; }

    public BankaController(BankovniSistemContext context)
    {
        Context = context;
    }

    [Route("DodajBanku")]
    [HttpPost]
    public async Task<ActionResult> DodajBanku([FromBody] BankaSlanje b)
    {
        try
        {
            var banka = new Banka()
            {
                Naziv = b.Naziv,
                Lokacija = b.Lokacija,
                Telefon = b.Telefon,
                BrZaposlenih = b.BrZaposlenih
            };

            await Context.banke.AddAsync(banka);
            await Context.SaveChangesAsync();
            return Ok($"Banka ID {banka.ID} dodata");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("UkupnaSumaNovca")]
    [HttpGet]
    public async Task<ActionResult> UkupnoNovca([FromQuery] int BankaID)
    {
        try
        {
            var suma = await Context.accounts
                .Where(b => b.Banka!.ID == BankaID)
                .SumAsync(k => k.StanjeRacuna + k.UkupnoPodignuto);

            return Ok(suma);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
