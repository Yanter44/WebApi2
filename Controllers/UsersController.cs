using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PopastNaStajirovku2.Entyties;
using PopastNaStajirovku2.Models;
using IdentityRole = Microsoft.AspNet.Identity.EntityFramework.IdentityRole;

namespace PopastNaStajirovku2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {      
        private readonly ILogger<UsersController> _logger;
     
        public Context _context;
        public UsersController(ILogger<UsersController> logger, Context context)
        {
            _logger = logger;
            _context = context;
           


        }

        [HttpGet]
        [Route("register")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var usersPage = _context.Users.ToList();
            return usersPage;
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<User>> CreateNewUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateNewUser), new { id = user.Id }, user);

        }
        [HttpDelete]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteCurrentUser(string email)
        {
            var userToDelete = _context.Users.FirstOrDefault(x => x.Email == email);
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return Ok("Пользователь был удален");
        }
        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangeRoleUser(string E)
        {
            
           
           

            return Ok();
        }
        

        


      
    }
}