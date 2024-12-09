using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class FakultetController : ControllerBase
{
    public StudijeContext Context { get; set; }

    public FakultetController(StudijeContext context)
    {
        Context = context;
    }

    [Route("DodajFakultet")]
    [HttpPost]
    public async Task<ActionResult> DodajFakultet([FromBody] FaxSlanje f)
    {
        try
        {
            var fakultet = new Fakultet()
            {
                Naziv = f.Naziv,
                Email = f.Email,
                Adresa = f.Adresa,
                Telefon = f.Telefon
            };

            await Context.fakulteti.AddAsync(fakultet);
            await Context.SaveChangesAsync();
            return Ok($"Fakultet sa ID {fakultet.ID} je dodat");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}