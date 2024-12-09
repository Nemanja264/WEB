using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class KuvarController : ControllerBase
{
    public RestoranContext Context { get; set; }

    public KuvarController(RestoranContext context)
    {
        Context = context;
    }

    [Route("DodajKuvara")]
    [HttpPost]
    public async Task<ActionResult> DodajKuvara([FromBody] KuvarSlanje k)
    {
        try
        {
            var kuvar = new Kuvar()
            {
                Ime = k.Ime,
                Prezime = k.Prezime,
                DatumRodjenja = k.DatumRodjenja,
                StrucnaSprema = k.StrucnaSprema
            };

            await Context.kuvari.AddAsync(kuvar);
            await Context.SaveChangesAsync();
            return Ok($"Kuvar sa ID {kuvar.ID} je dodat");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
