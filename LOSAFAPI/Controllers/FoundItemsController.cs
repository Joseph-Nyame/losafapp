using LOSAFAPI.Data;
using LOSAFAPI.Migrations.FoundItemsDb;
using LOSAFAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace LOSAFAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FoundItemsController : Controller
    {
        private readonly FoundItemsDbContext dbContext;
        private readonly AddRegisterItemsDbContext itemsContext;
        //private readonly JwtAuthenticationManager key;
        private readonly ReportItemsDbContext reportdbContext;
        private readonly UserApiDbContext userdbContext;



        public FoundItemsController(FoundItemsDbContext dbContext , AddRegisterItemsDbContext itemContext, ReportItemsDbContext reportdbContext, UserApiDbContext userdbContext)
        {
            this.dbContext = dbContext;
            this.itemsContext = itemContext;
            this.reportdbContext = reportdbContext;
            this.userdbContext = userdbContext;
        }
        [HttpPost]
        public async Task<IActionResult> FoundItem(FoundItemsRequest foundItem)
        {
            var itemId = foundItem.ItemId;
            var itemfounderid = foundItem.ItemFounderId;
            var allregistereditems = await itemsContext.ItemsRegisteration.ToListAsync();
            var itemfoundinfo = allregistereditems.Where(x => x.Id.ToString() == itemId).FirstOrDefault();
            var allusers = await userdbContext.Users.ToListAsync();
            var founderInfo = allusers.Where(x => x.Id.ToString() == itemfounderid).FirstOrDefault();
            var allItems = await reportdbContext.ReportItems.ToListAsync();
            var itemInfo = allItems.Where(m => m.ItemId.ToString() == itemId).FirstOrDefault();
            if(itemInfo != null)
            {
                itemInfo.founderContact = founderInfo.phone;
                itemInfo.founderName  = founderInfo.Name;
                itemInfo.Email = founderInfo.Email;
            }
            if(itemfoundinfo != null)
            {
                itemfoundinfo.founderContact = itemfounderid;
            }
            await itemsContext.SaveChangesAsync();

            await reportdbContext.SaveChangesAsync();

            if (itemInfo == null)
            {
                return NotFound();  
            }
            var ItemFound = new FoundItems()
            {
                Id = Guid.NewGuid(),
                ItemFounderId = itemfounderid.ToString(),
                UserItemId = itemInfo.UserId,
                ItemId = itemId
                };
            
            await dbContext.FoundItems.AddAsync(ItemFound);
            await dbContext.SaveChangesAsync();
            var resp = "item found success!";
            var reportedItems = await reportdbContext.ReportItems.ToListAsync();
            var reportedItem = reportedItems.Where(m => m.ItemId.ToString() == itemId).FirstOrDefault();
            reportdbContext.ReportItems.Remove(reportedItem);
            await reportdbContext.SaveChangesAsync();


            return Ok(resp);
        }
        [HttpGet]
        
        public async Task<IActionResult> FoundItems()
        {
           var foundItems = await dbContext.FoundItems.ToListAsync();
            List<Object> items = new List<Object>();
            foreach (var item in foundItems)
            {
                var itemdetails = await itemsContext.ItemsRegisteration.SingleOrDefaultAsync(x => x.Id.ToString() == item.ItemId.ToString());
                
               
                    items.Add(itemdetails);

                    
            }
            return Ok(items);
            
        }

      

    }
}
