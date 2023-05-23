using LOSAFAPI.Data;
using LOSAFAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LOSAFAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]    
    public class UserController : Controller
    {
        private readonly UserApiDbContext dbContext;

        public UserController(UserApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
           return  Ok( await dbContext.Users.ToListAsync());
           
        }



        [HttpPost]  
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = addUserRequest.Name,
                Email = addUserRequest.Email,
                phone = addUserRequest.phone,
                IDName = addUserRequest.IDName,
                IDNumber = addUserRequest.IDNumber,
                Password = addUserRequest.Password,
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            var key = "12345678$$$$0000";
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email ,user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var resp = tokenHandler.WriteToken(token);
            var tuple = Tuple.Create(resp, user);

            return Ok(tuple);
            

        }
        

    }


}
