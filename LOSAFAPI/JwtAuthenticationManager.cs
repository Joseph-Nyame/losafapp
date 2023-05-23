using LOSAFAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;

namespace LOSAFAPI
{
    

    public class JwtAuthenticationManager
    {
        private readonly string key;
        public JwtAuthenticationManager(string key)
        {
            this.key = key; 
        }
        private readonly UserApiDbContext dbContext;
        
        public string Authenticate(string email ,string password)
        {
            var users = dbContext.Users.ToList();
            if(!users.Any(u=>u.Email == email && u.Password ==password)) 
            { return null; }
             JwtSecurityTokenHandler tokenHandler= new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email ,email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
