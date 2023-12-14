using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi2.Jwt;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtTokenValidator _jwtTokenValidator;

        public TokenController(JwtTokenValidator jwtTokenValidator)
        {
            _jwtTokenValidator = jwtTokenValidator;
        }
        
        [HttpGet]
        public IActionResult ValidateToken([FromQuery] string token)
        {
            var result = _jwtTokenValidator.ValidateToken(token);
            return Ok(result);
        }
    }
}
