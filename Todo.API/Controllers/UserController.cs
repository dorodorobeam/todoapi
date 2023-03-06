using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Application.CQRS.User.Models;
using Todo.Domain.Entities;
using Todo.Persistence.Context;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> usermanager;
        private readonly SignInManager<User> signinmanager;

        public UserController(DatabaseContext context, IMapper mapper, UserManager<User> usermanager, SignInManager<User> signinmanager)
        {
            _context = context;
            _mapper = mapper;
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> Get()
        {
            return Ok(await _context.Users.Select(x => _mapper.Map<UserModel>(x)).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return BadRequest("User not found.");
            return Ok(_mapper.Map<UserModel>(user));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Updateuser(UserModel request)
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

            return Ok(true);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var dbuser = await _context.Users.FindAsync(id);
            if (dbuser == null)
                return BadRequest("user not found.");

            _context.Users.Remove(dbuser);
            await _context.SaveChangesAsync();

            return Ok(true);
        }
    }
}