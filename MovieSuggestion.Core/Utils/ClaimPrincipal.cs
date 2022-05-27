using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Utils
{
    public class ClaimPrincipal : ClaimsPrincipal
    {
        public virtual string NameIdentifier { get; }
        public List<string> Roles { get; }

        public ClaimPrincipal() : base()
        {

        }

        public ClaimPrincipal(ClaimsPrincipal principal) : base(principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal), "ClaimsPrincipal Identity can not be empty");
            if (principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                NameIdentifier = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            else
                NameIdentifier = "Anonymous";
            Roles = new List<string>();
            if (principal.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                foreach (var r in principal.Claims.Where(c => c.Type == ClaimTypes.Role))
                {
                    Roles.Add(r.Value);
                }
            }

        }
        public static string GenerateToken(string NameIdentifier,  List<string> Permissions)
        {
            var signingKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_JWT_KEY"));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, NameIdentifier)               
            };

            foreach (var p in Permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, p));
            }

            var creds = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1);
            var token = new JwtSecurityToken("MovieSuggestionApp", "MovieSuggestionAppClients", claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public static bool VerifyPassword(string Password, string Hash)
        {
            return BCrypt.Net.BCrypt.Verify(Password, Hash);
        }
    }
}
