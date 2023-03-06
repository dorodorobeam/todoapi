using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Application.CQRS.Todo.Models;
using Todo.Persistence.Context;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TodoController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoModel>>> Get()
        {
            return Ok(await _context.TodoList.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> Get(int id)
        {
            var todo = await _context.TodoList.FindAsync(id);
            if (todo == null)
                return BadRequest("Todo not found.");
            return Ok(new TodoModel
            {
                
            });
        }

        [HttpPost]
        public async Task<ActionResult<List<TodoModel>>> Addtodo(TodoModel todo)
        {
            _context.TodoList.Add(new Domain.Entities.TodoList
            {
               
            });
            await _context.SaveChangesAsync();

            return Ok(await _context.TodoList.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<TodoModel>>> Updatetodo(TodoModel request)
        {
            var dbtodo = await _context.TodoList.FindAsync(request.Id);
            if (dbtodo == null)
                return BadRequest("todo not found.");

            await _context.SaveChangesAsync();

            return Ok(await _context.TodoList.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TodoModel>>> Delete(int id)
        {
            var dbtodo = await _context.TodoList.FindAsync(id);
            if (dbtodo == null)
                return BadRequest("Todo not found.");

            _context.TodoList.Remove(dbtodo);
            await _context.SaveChangesAsync();

            return Ok(await _context.TodoList.ToListAsync());
        }
    }
}