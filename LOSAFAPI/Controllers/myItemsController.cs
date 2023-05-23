using LOSAFAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOSAFAPI.Models;

namespace LOSAFAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class myItemsController : Controller
    {
        private readonly AddRegisterItemsDbContext dbContext;
        private readonly UserApiDbContext _context;
        //private readonly JwtAuthenticationManager key;


        public myItemsController(AddRegisterItemsDbContext dbContext, UserApiDbContext _context)
        {
            this.dbContext = dbContext;
            this._context = _context;

        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetItems([FromRoute] Guid id)
        {
            
            var userID = id;
            var allItems = await dbContext.ItemsRegisteration.ToListAsync();
            var myitems = allItems.Where(d => d.UserId == userID.ToString()); 
            if (myitems == null)
            {
                return NotFound();
            }
            return Ok(myitems);

        }

        [HttpDelete]
       
        public async Task<IActionResult> RemoveItem(RemoveItem removeItem )
        {

            var itemId = removeItem.id;
            
            var item = await dbContext.ItemsRegisteration.SingleOrDefaultAsync(x => x.Id.ToString() == itemId.ToString());
            if (item == null)
            {
                return NotFound();
            }
            dbContext.ItemsRegisteration.Remove(item);
            await dbContext.SaveChangesAsync();
            var allItems = await dbContext.ItemsRegisteration.ToListAsync();
            return Ok();

        }
    }
}
