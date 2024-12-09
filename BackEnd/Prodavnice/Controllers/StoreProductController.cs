using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreProductController : ControllerBase
{
    public TrgovinaContext Context { get; set; }

    public StoreProductController(TrgovinaContext context)
    {
        Context = context;
    }

    [Route("DodajStoreProduct")]
    [HttpPost]
    public async Task<ActionResult> DodajStoreProduct([FromBody] StoreProduct sp)
    {
        try
        {
            var proizvod = await Context.proizvodi.FindAsync(sp.Proizvod.ID);
            var prodavnica = await Context.prodavnice.FindAsync(sp.Prodavnica.ID);

            if (proizvod == null || prodavnica == null)
            {
                return BadRequest("Greska pri dodavanju");
            }

            var StoreProductBaza = new StoreProduct()
            {
                Prodavnica = prodavnica,
                Proizvod = proizvod,
                Cena = sp.Cena,
                Kolicina = sp.Kolicina
            };

            await Context.storeProducts.AddAsync(StoreProductBaza);
            await Context.SaveChangesAsync();
            return Ok("Upisan proizvod u prodavnicu");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("DodajStoreProduct2/{proizvodId}/{prodavnicaId}/{cena}/{kolicina}")]
    [HttpPost]
    public async Task<ActionResult> DodajStoreProduct2(int proizvodId, int prodavnicaId, int cena, int kolicina)
    {
        try
        {
            var proizvod = await Context.proizvodi.FindAsync(proizvodId);
            var prodavnica = await Context.prodavnice.FindAsync(prodavnicaId);

            if (proizvod == null || prodavnica == null)
            {
                return BadRequest("Greska pri dodavanju");
            }

            var StoreProductBaza = new StoreProduct()
            {
                Prodavnica = prodavnica,
                Proizvod = proizvod,
                Cena = cena,
                Kolicina = kolicina
            };

            await Context.storeProducts.AddAsync(StoreProductBaza);
            await Context.SaveChangesAsync();
            return Ok("Upisan proizvod u prodavnicu");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("ExpiredProducts")]
    [HttpGet]
    public async Task<ActionResult> ExpiredProducts([FromQuery] DateTime date, [FromQuery] List<int> storeIds)
    {
        try
        {
            var expiredProducts = await Context
                .storeProducts
                .Include(p => p.Proizvod)
                .Include(p => p.Prodavnica)
                .Where(p => storeIds.Contains(p.Prodavnica.ID) && p.Proizvod.DatumIstekaRoka < date)
                .Select(p => new
                {
                    IDProdavnice = p.Prodavnica.ID,
                    ProdavniceIme = p.Prodavnica.Naziv,
                    ProizvodIme = p.Proizvod.Naziv,
                    ExpiryDate = p.Proizvod.DatumIstekaRoka
                })
                .ToListAsync();

            if (expiredProducts.Count == 0)
                return NotFound("Nema proizvoda sa isteklim rokom u datom datumu medju datim prodavnicama");

            return Ok(expiredProducts);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}