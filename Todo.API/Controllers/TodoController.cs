using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Application.CQRS.Todo.Models;
using Todo.Domain.Entities;
using Todo.Persistence.Context;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public TodoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TodoModel>>> GetUserTodo(int id)
        {
            return Ok(await _context.TodoList
                                    .Include(x => x.User)
                                    .Where(x => x.User.Id == id)
                                    .Select(x => _mapper.Map<TodoModel>(x))
                                    .ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Addtodo(TodoModel todo)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == todo.UserId);
            if (user == null)
                return BadRequest("User not found!");

            var data = _mapper.Map<TodoList>(todo);
            data.User = user;
            _context.TodoList.Add(data);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Updatetodo(TodoModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
                return BadRequest("User not found!");

            var dbtodo = await _context.TodoList.FindAsync(request.Id);
            if (dbtodo == null)
                return BadRequest("todo not found.");

            dbtodo.Title = request.Title;
            dbtodo.Remarks = request.Remarks;
            dbtodo.Status = request.Status;
            dbtodo.Deadline = request.Deadline;
            dbtodo.DateCompleted = request.DateCompleted.HasValue ? request.DateCompleted : null;

            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var dbtodo = await _context.TodoList.FindAsync(id);
            if (dbtodo == null)
                return BadRequest("Todo not found.");

            _context.TodoList.Remove(dbtodo);
            await _context.SaveChangesAsync();

            return Ok(true);
        }
    }
}