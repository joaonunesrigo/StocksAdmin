using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StocksAdmin.Api.Services.User
{
    public class JwtTokenService
    {
        public static string GenerateToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ExternalApiConfig.SecretJWT);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = GenerateClaimsIdentity(email, userId),
                Expires = DateTime.UtcNow.AddHours(2)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaimsIdentity(string email, long userId)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Email, email));
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            return ci;
        }

        public long GetUserIdFromClaims(ClaimsPrincipal user)
        {
            return long.Parse(user.FindFirst("sub")?.Value ?? "0");
        }
    }
}
