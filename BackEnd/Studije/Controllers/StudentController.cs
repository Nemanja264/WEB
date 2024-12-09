using Models;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    public StudijeContext Context { get; set; }

    public StudentController(StudijeContext context)
    {
        Context = context;
    }

    [Route("DodajStudenta")]
    [HttpPost]
    public async Task<ActionResult> DodajStudenta([FromBody] StudentSlanje s)
    {
        try
        {
            var fax = await Context.fakulteti.FindAsync(s.idFakulteta);
            if (fax == null)
                return NotFound("Fakultet not found");

            var student = new Student()
            {
                brIndexa = s.brIndexa,
                Ime = s.Ime,
                Prezime = s.Prezime,
                DatumRodjenja = s.DatumRodjenja,
                ZavrsenaSrednja = s.ZavrsenaSrednja,
                Fakultet = fax
            };

            if (fax.Studenti == null)
                fax.Studenti = new List<Student>();

            fax.Studenti.Add(student);

            await Context.studenti.AddAsync(student);
            await Context.SaveChangesAsync();
            return Ok($"Student sa ID {student.ID} je dodat");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("UpisiSmer")]
    [HttpPost]
    public async Task<ActionResult> DodajNaSmer([FromBody] UpisSlanje u)
    {
        try
        {
            var student = await Context.studenti.FindAsync(u.idStudenta);
            if (student == null)
                return NotFound("Student not found");

            var smer = await Context.smerovi.FindAsync(u.idSmera);
            if (smer == null)
                return NotFound("Smer not found");

            var fax = await Context.fakulteti.FindAsync(u.idFakulteta);
            if (fax == null)
                return NotFound("Fakultet not found");

            var upis = new Upis()
            {
                DatumUpisa = u.datumUpisa,
                ESPB = u.espb,
                Fakultet = fax,
                Smer = smer,
                Student = student
            };

            student.Smer = smer;

            await Context.upisi.AddAsync(upis);
            await Context.SaveChangesAsync();
            return Ok($"Student sa ID {student.ID} je dodat na smer {smer.Naziv}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("IzmeniStudenta")]
    [HttpPut]
    public async Task<ActionResult> Izmeni([FromQuery] int StudentID, [FromBody] StudentIzmena si)
    {
        try
        {
            var fax = await Context.fakulteti.FindAsync(si.FakultetID);
            if (fax == null)
                return NotFound("Fakultet not found");

            var student = await Context.studenti.FindAsync(StudentID);
            if (student == null)
                return NotFound("Student not found");

            var smer = await Context.smerovi.FindAsync(si.SmerID);
            if (smer == null)
                return NotFound("Smer not found");

            student.Ime = si.Ime;
            student.Prezime = si.Prezime;
            student.DatumRodjenja = si.datumRodjenja;
            student.Fakultet = fax;
            student.ZavrsenaSrednja = si.Srednja;
            student.Smer = smer;

            await Context.SaveChangesAsync();
            return Ok("Student izmenjen");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("DvaDatuma")]
    [HttpGet]
    public async Task<ActionResult> NadjiStudente([FromQuery] int SmerID, [FromQuery] DateTime Start, [FromQuery] DateTime End)
    {
        try
        {
            var smer = await Context.smerovi.FindAsync(SmerID);
            if (smer == null)
                return NotFound("Smer not found");

            var studenti = await Context.upisi
                    .Include(s => s.Student)
                    .Where(s => s.Smer.ID == SmerID && (s.DatumUpisa > Start && s.DatumUpisa < End))
                    .Select(s => new
                    {
                        BrojIndexa = s.Student.brIndexa,
                        Ime = s.Student.Ime,
                        Prezime = s.Student.Prezime,
                        Smer = s.Smer.Naziv,
                        DatumUpisa = s.DatumUpisa
                    })
                    .ToListAsync();

            return Ok(studenti);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("ProsekESPB")]
    [HttpGet]
    public async Task<ActionResult> VratiProsek([FromQuery] int SmerID)
    {
        try
        {
            var smer = await Context.smerovi.FindAsync(SmerID);
            if (smer == null)
                return NotFound("Smer not found");

            var prosek = await Context.upisi
                .Where(u => u.Smer.ID == SmerID)
                .AverageAsync(u => u.ESPB);

            return Ok($"Prosek ESPB: {prosek}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
