using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class SmerController : ControllerBase
{
    public StudijeContext Context { get; set; }

    public SmerController(StudijeContext context)
    {
        Context = context;
    }

    [Route("DodajSmer")]
    [HttpPost]
    public async Task<ActionResult> DodajSmer([FromBody] SmerSlanje s)
    {
        try
        {
            var fax = await Context.fakulteti.FindAsync(s.idFakulteta);
            if (fax == null)
                return NotFound("Fakultet not found");

            var smer = new Smer()
            {
                Naziv = s.Naziv,
                Fakultet = fax
            };

            if (fax.Smerovi == null)
                fax.Smerovi = new List<Smer>();

            fax.Smerovi.Add(smer);

            await Context.smerovi.AddAsync(smer);
            await Context.SaveChangesAsync();
            return Ok($"Smer sa ID {smer.ID} je dodat");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}