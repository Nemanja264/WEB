using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class UgovorController : ControllerBase
{
    public FirmeContext Context { get; set; }

    public UgovorController(FirmeContext context)
    {
        Context = context;
    }

    [Route("NadjiZaposlene")]
    [HttpGet]
    public async Task<ActionResult> NadjiViseFirmi([FromQuery] DateTime datum)
    {
        try
        {
            var zaposleni = await Context.zaposleni
                    .Where(u => u.Ugovori.Count(eng => eng.datumPotpisivanja >= datum) > 1)
                    .ToListAsync();

            if (zaposleni.Count == 0)
                return NotFound("Nisu nadjeni takvi zaposleni");

            return Ok(zaposleni);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
