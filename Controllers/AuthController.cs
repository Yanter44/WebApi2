using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PopastNaStajirovku2.Models;
using Swashbuckle.AspNetCore.Annotations;
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
         public static User user = new User();


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
         //   string PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.UserPassword);
            user.PasswordHash = request.UserPassword;
            user.Login = request.UserLogin;
            user.Email = request.Email;
            user.Age = request.Age;

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d4d202f4210bf8335095eeb822a24f0c"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.Email, user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
           
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
       
   
        //public IActionResult ValidateToken([FromQuery] string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes("d4d202f4210bf8335095eeb822a24f0c");
        //    var validationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidIssuer = "YongeApi",
        //        ValidateAudience = true,
        //        ValidAudience = "users",
        //        ValidateLifetime = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key)
        //    };

        //    SecurityToken validatedToken;
        //    try
        //    {
        //        var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        //        return Ok("Токен валиден");
        //    }
        //    catch (Exception)
        //    {
        //        return Unauthorized("Токен невалиден");
        //    }
        //}

    }
}
