using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopastNaStajirovku2.Entyties;
using PopastNaStajirovku2.Models;

namespace PopastNaStajirovku2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {      
        private readonly ILogger<UsersController> _logger;
        public List<User> ListUsers = new List<User>();
        public Context _context;
        public UsersController(ILogger<UsersController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("register")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers(int page = 1, int pagesize = 10)
        {
            var allusers = ListUsers.Count;
            var allpages = (int)Math.Ceiling((decimal)allusers / pagesize);
            var usersPerPage = ListUsers.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            return usersPerPage;
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateNewUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateNewUser), new { id = user.Id }, user);

        }
        

        


      
    }
}