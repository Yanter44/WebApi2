using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using PopastNaStajirovku2.Entyties;
using PopastNaStajirovku2.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi2.Models;

namespace PopastNaStajirovku2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        public  Context _context;
      
        public AuthController(Context context)
        {
            _context = context;
            
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            //   string PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.UserPassword);
            var user = new User();
            user.PasswordHash = request.UserPassword;
            user.Login = request.UserLogin;
            user.Email = request.Email;
            user.Age = request.Age;
            user.Role = Roles.Roless.User.ToString();

            _context.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }
        [HttpPost]
        [Route("loging")]
        public async Task<ActionResult<User>> Loging(string userEmail, string Password)
        {           
                var allusers = _context.Users.ToList();

                for (int i = 0; i < allusers.Count; i++)
                {
                  if (allusers[i].Email == userEmail)
                  {
                    if (allusers[i].PasswordHash == Password)
                    {
                      var userRole = allusers[i].Role;
                      var token = GenerateJwtToken(allusers[i],userRole); // вызов метода генерации токена
                      return Ok(new { token }); 

                    }
                  }
               
                }
                return BadRequest();
            
        }
        private string GenerateJwtToken(User user, string userRole)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d4d202f4210bf8335095eeb822a24f0c"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Roles.Roless roletoken = (Roles.Roless)Enum.Parse(typeof(Roles.Roless), userRole);
         
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.Email, user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(ClaimsIdentity.DefaultRoleClaimType, roletoken.ToString() )
           
            };

            var token = new JwtSecurityToken(
                issuer: "YongeApi",
                audience: "users",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
