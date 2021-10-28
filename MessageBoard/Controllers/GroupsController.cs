using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoard.Models;
using System.Linq;
using System;
using Swashbuckle.AspNetCore.Annotations;

namespace MessageBoard.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class GroupsController : ControllerBase
  {
    private readonly MessageBoardContext _db;

    public GroupsController(MessageBoardContext db)
    {
      _db = db;
    }

    // GET api/groups
    /// <summary>
    /// Gets groups.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Group
    ///     {
    ///        "id": 1,
    ///        "name": "Group1",
    ///        "messages": []
    ///     }
    ///
    /// </remarks>
    /// <param name="name"></param>
    /// <returns>A specific list of groups with that name.</returns>
    /// <response code="201">Returns the groups</response>
    /// <response code="400">If the item is null</response>   
    [HttpGet]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<Group>>> Get(string name)
    {
      var query = _db.Groups.AsQueryable();
      if (name != null)
      {
        query = query.Where(entry => entry.Name.Contains(name));
      }
      return await query.ToListAsync();
    }

    // POST api/groups
    [HttpPost]
    public async Task<ActionResult<Group>> Post(Group group)
    {
      _db.Groups.Add(group);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetGroup), new { id = group.GroupId }, group);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetGroup(int id)
    {
      var group = await _db.Groups.FindAsync(id);

      if (group == null)
      {
        return NotFound();
      }

      return group;
    }

    // PATCH: api/Groups/5
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, Group group)
    {
      if (id != group.GroupId)
      {
        return BadRequest();
      }

      _db.Entry(group).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GroupExists(id))
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
    public async Task<IActionResult> DeleteGroup(int id)
    {
      var group = await _db.Groups.FindAsync(id);
      if (group == null)
      {
        return NotFound();
      }

      _db.Groups.Remove(group);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool GroupExists(int id)
    {
      return _db.Groups.Any(e => e.GroupId == id);
    }
  }
}