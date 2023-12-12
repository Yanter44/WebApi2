using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PopastNaStajirovku2.Models;
using WebApi2.Models;

namespace PopastNaStajirovku2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
         public static User user = new User();


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            string PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.UserPassword);
            user.PasswordHash = PasswordHash;
            user.Name = request.UserName;
            return null;
        }
      
    }
}
