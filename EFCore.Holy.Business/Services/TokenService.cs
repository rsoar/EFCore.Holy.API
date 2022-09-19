
using EFCore.Holy.Data.Config;
using EFCore.Holy.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EFCore.Holy.Business.Services
{
    public class TokenService
    {
        public static string GenerateToken(Manager manager)
        {
            byte[] key = Encoding.ASCII.GetBytes(TokenSettings.Secret);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var subject = new ClaimsIdentity(new[]
            {
                new Claim("id", manager.Id.ToString()),
                new Claim("name", manager.Name),
                new Claim("role", manager.Role.ToString()),
                new Claim("churchId", manager.IdChurch.ToString())
            }); ;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static Claim GetProperty(IEnumerable<Claim> claims, string key)
        {
            return claims.SingleOrDefault(c => c.Type == key || c.Type.Contains(key));
        }
    }
}
