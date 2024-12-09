using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdavnicaController : ControllerBase
{
    public TrgovinaContext Context { get; set; }

    public ProdavnicaController(TrgovinaContext context)
    {
        Context = context;
    }

    [Route("DodajProdavnicu")]
    [HttpPost]
    public async Task<ActionResult> DodajProdavnicu([FromBody] Prodavnica store)
    {
        try
        {
            await Context.prodavnice.AddAsync(store);
            await Context.SaveChangesAsync();
            return Ok($"Dodata prodavnica sa ID = {store.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("IzmeniProizvod/{idProdavnice}/{idProizvoda}/{cena}/{kolicina}")]
    [HttpPut]
    public async Task<ActionResult> IzmeniProizvod(int idProdavnice, int idProizvoda, int cena, int kolicina)
    {
        try
        {
            var prodavnica = await Context.prodavnice.FindAsync(idProdavnice);

            if (prodavnica == null)
                return BadRequest("Prodavnica sa ovim ID ne postoji");

            var storepr = await Context.storeProducts
                        .FirstOrDefaultAsync(sp => sp.Prodavnica.ID == idProdavnice && sp.Proizvod.ID == idProizvoda);

            if (storepr == null)
                return BadRequest("Proizvod sa ovim ID ne postoji");

            storepr.Cena = cena;
            storepr.Kolicina = kolicina;

            await Context.SaveChangesAsync();
            return Ok(storepr);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("KupiProizvod/{idProdavnice}/{idProizvoda}")]
    [HttpPost]
    public async Task<ActionResult> Kupi(int idProizvoda, int idProdavnice)
    {
        try
        {
            var prodavnica = await Context.prodavnice.FindAsync(idProdavnice);

            if (prodavnica == null)
                return BadRequest("Prodavnica sa ovim ID ne postoji");

            var proizvod = await Context.storeProducts.FirstOrDefaultAsync(sp => sp.Prodavnica.ID == idProdavnice && sp.Proizvod.ID == idProizvoda);

            if (proizvod == null)
                return BadRequest("Proizvod sa ovim ID ne postoji");

            proizvod.Kolicina--;

            if (proizvod.Kolicina == 0)
            {
                string nazivProizvoda = proizvod.Proizvod.Naziv;
                Context.storeProducts.Remove(proizvod);
                await Context.SaveChangesAsync();

                return Ok($"Proizvod:{nazivProizvoda} vise nema zaliha u {prodavnica.Naziv}");
            }

            await Context.SaveChangesAsync();
            return Ok($"Trenutno stanje {proizvod.Proizvod.Naziv} u radnji {prodavnica.Naziv} je {proizvod.Kolicina}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("ProdavniceGet")]
    [HttpGet]
    public async Task<ActionResult> GetStores()
    {
        try
        {
            var pricyProducts = await Context
                .prodavnice
                .ToListAsync();

            return Ok(pricyProducts);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
