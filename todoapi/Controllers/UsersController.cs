using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }
[HttpGet]
public async Task<ActionResult<IEnumerable<User>>> GetUsers()
{
    return await _context.Users
                         .Include(u => u.Tasks) // Ensure tasks are included
                         .ToListAsync();
}
        // GET: api/Users
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        // {
        //     return await _context.Users.ToListAsync();
        // }

        // GET: api/Users/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<User>> GetUser(int id)
        // {
        //     var user = await _context.Users.FindAsync(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return user;
        // }
        [HttpGet("{id}")]
public async Task<ActionResult<User>> GetUser(int id)
{
    var user = await _context.Users
                             .Include(u => u.Tasks) // Ensure tasks are loaded
                             .FirstOrDefaultAsync(u => u.Id == id);
    
    if (user == null)
    {
        return NotFound();
    }
    return user;
}

        // POST: api/Users
        [HttpPost]
public async Task<ActionResult<User>> PostUser(User user)
{
    if (user.Tasks != null)
    {
        foreach (var task in user.Tasks)
        {
            task.UserId = user.Id; // Ensure the foreign key is set
        }
    }

    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
}

        // PUT: api/Users/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutUser(int id, User user)
        // {
        //     if (id != user.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(user).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!_context.Users.Any(e => e.Id == id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }
[HttpPut("{id}")]
public async Task<IActionResult> PutUser(int id, User user)
{
    if (id != user.Id)
    {
        return BadRequest();
    }

    _context.Entry(user).State = EntityState.Modified;

    // Ensure existing tasks are updated
    foreach (var task in user.Tasks ?? new List<TaskItem>())
    {
        _context.Entry(task).State = task.Id == 0 ? EntityState.Added : EntityState.Modified;
    }

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Users.Any(e => e.Id == id))
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
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}