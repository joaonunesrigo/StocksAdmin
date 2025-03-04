using Microsoft.IdentityModel.Tokens;
using StocksAdmin.Communication.Requests.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StocksAdmin.Api.Services.User
{
    public class JwtTokenService
    {
        public static string GenerateToken(UserLoginRequest userLoginRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ExternalApiConfig.SecretJWT);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = GenerateClaimsIdentity(userLoginRequest),
                Expires = DateTime.UtcNow.AddHours(2)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaimsIdentity(UserLoginRequest userLoginRequest)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Email, userLoginRequest.Email));
            return ci;
        }
    }
}
