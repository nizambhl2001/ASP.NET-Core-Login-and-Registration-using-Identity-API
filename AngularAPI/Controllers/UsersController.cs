using AngularAPI.Data;
using AngularAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UsersController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObject)
        {
            if(userObject == null)
            {
                return BadRequest();
            }
            var user = await _applicationDbContext.users.FirstOrDefaultAsync(x => x.Username == userObject.Username && x.Password == userObject.Password);
            if (user == null)
            {
                return NotFound(new { Message = "User Not Found!" });
            }
            return Ok(new {Message = "Login Success!"});
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
           if(userObj == null)
            {
                return BadRequest();

            }
            await _applicationDbContext.users.AddAsync(userObj);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(new {Message = "User Registerd!"});
        }
    }   
}
