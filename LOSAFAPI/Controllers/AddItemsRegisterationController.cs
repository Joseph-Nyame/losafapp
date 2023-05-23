using LOSAFAPI.Data;
using LOSAFAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;



namespace LOSAFAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AddItemsRegisterationController : Controller
    {
        private readonly AddRegisterItemsDbContext dbContext;
        //private readonly JwtAuthenticationManager key;
        private readonly UserApiDbContext _context;
        private readonly IWebHostEnvironment _environment;


        public AddItemsRegisterationController(AddRegisterItemsDbContext dbContext , UserApiDbContext _context,IWebHostEnvironment environment)
        {
            this.dbContext = dbContext;
            this._context = _context;
            this._environment = environment;

        }

        

        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm]AddItems itemRegisteration)
        {

            var useremail = itemRegisteration.email;
            var userInfo = await _context.Users.FirstOrDefaultAsync(u => u.Email == useremail);

            var filet = itemRegisteration.file;
            string filepath = getFilePath(filet.FileName);
           // return Ok(filepath);
           // byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
            
            string imagepath = filepath ;
            using (FileStream filestream= System.IO.File.Create(imagepath))
            {
                await filet.CopyToAsync(filestream);
            }
           //return Ok(imageArray);

            var item = new AddItemsRegisteration();


                item.Id = Guid.NewGuid();
                item.UserId = itemRegisteration.UserId;
                item.ItemCategory = itemRegisteration.ItemCategory;
                item.ItemDescription = itemRegisteration.ItemDescription;
                item.ItemImage = filet.FileName;
            item.ItemName = itemRegisteration.ItemName;
                item.UniqueDetail = itemRegisteration.UniqueDetail;
            item.founderContact = "null";
                
            

            await dbContext.ItemsRegisteration.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return Ok(item);

        }
        [NonAction]
        private string getFilePath(string imgName)
        {
            var result = this._environment.WebRootPath+"\\uploads\\img\\" + imgName;
            return result;
        }
        [NonAction]
        private string getImg(string imgName)
        {
            string imgUrl = string.Empty;
            string HostUrl = "https://localhost:44384/";
            
         
            imgUrl =HostUrl+"/uploads/img/"+imgName;
            
            return imgUrl;
        }
        
    }
}
