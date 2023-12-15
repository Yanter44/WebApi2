using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApi2.Jwt
{
    public class JwtTokenValidator
    {
        public bool ValidateToken( string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("d4d202f4210bf8335095eeb822a24f0c");
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "YongeApi",
                ValidateAudience = true,
                ValidAudience = "users",
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            SecurityToken validatedToken;
            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                return true;
               
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
