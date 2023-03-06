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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return BadRequest("User not found.");
            return Ok(new UserModel
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Name = user.Name,
            });
        }

        [HttpPost]
        public async Task<ActionResult<List<UserModel>>> Adduser(UserModel user)
        {
            _context.Users.Add(new Domain.Entities.User
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Middlename = user.Middlename,
                Prefix = user.Prefix,
                Suffix = user.Suffix,
                Specialization = user.Specialization,
                Gender = user.Gender,
                CivilStatus = user.CivilStatus,
                Birthday = user.Birthday,
            });
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<UserModel>>> Updateuser(UserModel request)
        {
            var dbuser = await _context.Users.FindAsync(request.Id);
            if (dbuser == null)
                return BadRequest("user not found.");

            dbuser.Firstname = request.Firstname;
            dbuser.Lastname = request.Lastname;
            dbuser.Middlename = request.Middlename;
            dbuser.Prefix = request.Prefix;
            dbuser.Suffix = request.Suffix;
            dbuser.Specialization = request.Specialization;
            dbuser.Gender = request.Gender;
            dbuser.CivilStatus = request.CivilStatus;

            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserModel>>> Delete(int id)
        {
            var dbuser = await _context.Users.FindAsync(id);
            if (dbuser == null)
                return BadRequest("user not found.");

            _context.Users.Remove(dbuser);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }
    }
}