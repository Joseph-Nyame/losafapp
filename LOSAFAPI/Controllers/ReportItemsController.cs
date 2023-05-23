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
    public class ReportItemsController : Controller
    {
        private readonly ReportItemsDbContext dbContext;
        private readonly UserApiDbContext _dbContext;
        private readonly AddRegisterItemsDbContext itemsContext;




        public ReportItemsController(ReportItemsDbContext dbContext , UserApiDbContext _dbContext ,AddRegisterItemsDbContext itemsContext)
        {
            this.dbContext = dbContext;
            this._dbContext = _dbContext;
            this.itemsContext = itemsContext;
        }



        //public ReportItemsController(AddRegisterItemsDbContext itemContext)
        //{
        //    this.itemContext = itemContext;

        //}

        [HttpPost]
        public async Task<IActionResult> ReportItem(ReportItemsRequest reportItemsRequest)
        {

            var itemId = reportItemsRequest.ItemId;
            var allItems = await itemsContext.ItemsRegisteration.ToListAsync();
            var itemInfo  = allItems.Where(m => m.Id.ToString() == itemId).FirstOrDefault();
            if(itemInfo == null)
            {
                return NotFound();
            }


            var ItemReport = new ReportItems()
            {
                Id = Guid.NewGuid(),
                UserId = itemInfo.UserId,
                ItemId = itemId,
                image = itemInfo.ItemImage,
                founderContact = "null",
                Email ="null",
                founderName = "null",   
                ItemCategory = itemInfo.ItemCategory,
                ItemDescription = itemInfo.ItemDescription,
                UniqueDetail = itemInfo.UniqueDetail,

            };

            await dbContext.ReportItems.AddAsync(ItemReport);
            await dbContext.SaveChangesAsync();
            var resp = "item report success!";

            return Ok(resp);


        }
        [HttpGet]
        public async Task<IActionResult> LostItems()
        {
            var lostItems = await dbContext.ReportItems.ToListAsync();
            return Ok(lostItems);
        }
    }
}
