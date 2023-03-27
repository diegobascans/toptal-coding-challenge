using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Data;
using Backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Auth
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly string _key;

        public JwtAuthenticationService(string key)
        {
            _key = key;
        }

        public string Authenticate(User user)
        { 
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Role", user.Role.ToString("d")),
                    new Claim("Username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static int GetUserIdFromToken(HttpContext httpContext)
        {
            var tokenS = GetToken(httpContext);
            return int.Parse(tokenS.Claims.First(claim => claim.Type == "UserId").Value);
        }

        public static int GetRoleFromToken(HttpContext httpContext)
        {
            var tokenS = GetToken(httpContext);
            return int.Parse(tokenS.Claims.First(claim => claim.Type == "Role").Value);
        }

        private static JwtSecurityToken GetToken(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.Replace("Bearer ", string.Empty));
            return jsonToken as JwtSecurityToken;
        }
    }
}