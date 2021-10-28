using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoard.Models;
using System.Linq;

namespace MessageBoard.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PersonsController : ControllerBase
  {
    private readonly MessageBoardContext _db;

    public PersonsController(MessageBoardContext db)
    {
      _db = db;
    }

    // GET api/account
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> Get(string name)
    {
      var query = _db.Persons.AsQueryable();
      if (name != null)
      {
        query = query.Where(entry => entry.Name.Contains(name));
      }
      return await query.ToListAsync();
    }

    // POST api/Persons
    [HttpPost]
    public async Task<ActionResult<Person>> Post(Person person)
    {
      _db.Persons.Add(person);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetPerson), new { id = person.PersonId }, person );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
      var person = await _db.Persons.FindAsync(id);

      if (person == null)
      {
        return NotFound();
      }

      return person;
    }

    // PATCH: api/Messages/5
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, Person person)
    {
      if (id != person.PersonId)
      {
        return BadRequest();
      }

      _db.Entry(person).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PersonExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
      var person = await _db.Persons.FindAsync(id);
      if (person == null)
      {
        return NotFound();
      }

      _db.Persons.Remove(person);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool PersonExists(int id)
    {
      return _db.Persons.Any(e => e.PersonId == id);
    }
  }
}