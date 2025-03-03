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
    public class TaskItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            return await _context.TaskItems.Include(t => t.User).ToListAsync();
        }

        // GET: api/TaskItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }
            return taskItem;
        }

        // POST: api/TaskItems
        // [HttpPost]
        // public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        // {
        //     taskItem.Normalize();
        //     _context.TaskItems.Add(taskItem);
        //     await _context.SaveChangesAsync();
        //     return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
        // }
        [HttpPost]
public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
{
    var userExists = await _context.Users.AnyAsync(u => u.Id == taskItem.UserId);
    if (!userExists)
    {
        return BadRequest("Invalid UserId.");
    }

    taskItem.Normalize();
    _context.TaskItems.Add(taskItem);
    await _context.SaveChangesAsync();
    
    return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
}

        // PUT: api/TaskItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaskItems.Any(e => e.Id == id))
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

        // DELETE: api/TaskItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}