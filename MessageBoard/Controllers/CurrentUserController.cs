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
  public class CurrentUserController : ControllerBase
  {
    private readonly MessageBoardContext _db;

    public CurrentUserController(MessageBoardContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CurrentUser>>> Get(string name)
    {
      var query = _db.CurrentUser.AsQueryable();
      return await query.ToListAsync();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, CurrentUser currentUser)
    {
      if (id != currentUser.CurrentUserId)
      {
        return BadRequest();
      }

      _db.Entry(currentUser).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CurrentUserExists(id))
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
    private bool CurrentUserExists(int id)
    {
      return _db.Groups.Any(e => e.GroupId == id);
    }
  }
}
