using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.CQRS.User.Models;
using Todo.Domain.Entities;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> usermanager;
        private readonly SignInManager<User> signinmanager;

        public AuthController(UserManager<User> usermanager, SignInManager<User> signinmanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await signinmanager.PasswordSignInAsync(user.UserName, user.Password, false, false);

                if (result.Succeeded)
                    return Ok(result);
                else
                    return BadRequest("Invalid Credentials!");
            }

            return BadRequest("There is an Error on the Request!");
        }

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                var userData = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Prefix = user.Prefix,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Middlename = user.Middlename,
                    Suffix = user.Suffix,
                    Gender = user.Gender,
                    CivilStatus = user.CivilStatus,
                    Birthday = user.Birthday,
                    Specialization = user.Specialization,
                };
                var result = await usermanager.CreateAsync(userData, user.Password);

                if (result.Succeeded)
                {
                    // await signinmanager.SignInAsync(userData, isPersistent: false);
                    return Ok(true);
                }
                else
                {
                    var hasError = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        hasError = $"{hasError} {error.Description},";
                        ModelState.AddModelError("", error.Description);
                    }

                    return BadRequest(hasError);
                }
            }

            return BadRequest("There is an Error on the Request!");
        }
    }
}