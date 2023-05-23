using LOSAFAPI.Data;
using LOSAFAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Identity.Client;
using LOSAFAPI;

namespace LOSAFAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LoginController : Controller
    {
        private readonly UserApiDbContext dbContext;
        //private readonly JwtAuthenticationManager key;
        

        public LoginController(UserApiDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login loginrequest)
        {
         
            var email = loginrequest.Email;   
            var password = loginrequest.Password;
            var users = await dbContext.Users.ToListAsync();
            var alreadyExists = users.Any(x => x.Email == email && x.Password == password);
            var auth = users.FirstOrDefault(x => x.Email == email);

            var userInfo = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            var UserId = userInfo.Id.ToString();
            var key = "12345678$$$$0000";
            if (alreadyExists == null)
            {
                var res = "user not found";
                return Ok(res);

            }
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
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
            var resp = tokenHandler.WriteToken(token);
            var tuple = Tuple.Create(resp,auth,UserId);

            // return the tuple in an OK response
            return Ok(tuple);
        }


        //[Authorize]
        //public IActionResult items()
        //{
        //    var name = "cell";
        //    return Ok(name);
        //}


    }


}
