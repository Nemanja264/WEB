using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class KnjigaController : ControllerBase
{
    public BibliotekaContext Context { get; set; }

    public KnjigaController(BibliotekaContext context)
    {
        Context = context;
    }

    [Route("DodajKnjigu")]
    [HttpPost]
    public async Task<ActionResult> DodajKnjigu([FromBody] Knjiga knjiga)
    {
        try
        {
            await Context.knjige.AddAsync(knjiga);
            await Context.SaveChangesAsync();
            return Ok($"Knjiga sa ID {knjiga.ID} je dodat");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PronadjiKnjige")]
    [HttpGet]
    public async Task<ActionResult> PronadjiManjakKnjiga()
    {
        try
        {
            var knjige = await Context.knjige
                .Where(k => k.BrDostupnih < 5)
                .ToListAsync();

            if (knjige == null)
                return NotFound("Nisu pronadjene knjige koje imaju manje od 5 preostala primerka");

            return Ok(knjige);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
