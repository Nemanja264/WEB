using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class ZaposleniController : ControllerBase
{
    public FirmeContext Context { get; set; }

    public ZaposleniController(FirmeContext context)
    {
        Context = context;
    }

    [Route("DodajZaposlenog")]
    [HttpPost]
    public async Task<ActionResult> DodajZaposlenog([FromBody] Zaposleni zaposlen)
    {
        try
        {
            await Context.zaposleni.AddAsync(zaposlen);
            await Context.SaveChangesAsync();
            return Ok($"Zaposleni sa ID {zaposlen.ID} je dodat");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
