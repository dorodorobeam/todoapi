using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Application.CQRS.User.Models;
using Todo.Persistence.Context;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> Get()
        {
            return Ok(await _context.Users.Select(x => new UserModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Name = x.Name,
            }).ToListAsync());
        }
    }
}