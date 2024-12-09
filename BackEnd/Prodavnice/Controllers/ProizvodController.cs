using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class ProizvodController : ControllerBase
{
    public TrgovinaContext Context { get; set; }

    public ProizvodController(TrgovinaContext context)
    {
        Context = context;
    }

    [Route("DodajProizvod")]
    [HttpPost]
    public async Task<ActionResult> DodajProdavnicu([FromBody] Proizvod product)
    {
        try
        {
            await Context.proizvodi.AddAsync(product);
            await Context.SaveChangesAsync();
            return Ok($"Dodat proizvod sa ID = {product.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}