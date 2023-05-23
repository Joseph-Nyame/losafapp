using LOSAFAPI.Data;
using LOSAFAPI.Migrations.FoundItemsDb;
using LOSAFAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;


namespace LOSAFAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LogItemController : Controller
    {
        private readonly FoundItemsDbContext dbContext;
        private readonly AddRegisterItemsDbContext itemsContext;
        //private readonly JwtAuthenticationManager key;
        private readonly ReportItemsDbContext reportdbContext;
        private readonly UserApiDbContext userContext;


        public LogItemController(FoundItemsDbContext dbContext, AddRegisterItemsDbContext itemContext, ReportItemsDbContext reportdbContext, UserApiDbContext userContext)
        {
            this.dbContext = dbContext;
            this.itemsContext = itemContext;
            this.reportdbContext = reportdbContext;
            this.userContext = userContext; 
        }
  

        [HttpPost]
        public async Task<IActionResult> LogItem([FromForm]LogItem LogItem)
        {
            var itemname = LogItem.ItemName;
            var itemcategory = LogItem.ItemCategory;
            var uniquedetail = LogItem.UniqueDetail;
            var itemDescription = LogItem.ItemDescription;
            var userItemId = LogItem.UserId;
            var ItemFounderId = LogItem.ItemFounderId;
            var allItems = await itemsContext.ItemsRegisteration.ToListAsync();
            var foundItems = await dbContext.FoundItems.ToListAsync();
            
            Regex regex = new Regex(@"\d+"); // a Regex pattern to match any sequence of digits

            Match match = regex.Match(itemDescription);  // find the first match of the pattern in the text
            Match match2 = regex.Match(uniquedetail);  // find the first match of the pattern in the text

            if (match.Success || match2.Success )
            {
                var number = int.Parse(match.Value); 
                var number2 = int.Parse(match2.Value); 
                var numbers = Tuple.Create(number, number2); 
                var itemscheck = itemsContext.ItemsRegisteration.ToList();
                var unique = itemscheck.Select(p => p.UniqueDetail);
                var desc = itemscheck.Select(p => p.ItemDescription);
                var arrayitems = itemscheck.ToArray();
                
                foreach (var item in unique)
                {
                    if (item.ToString().Contains(number2.ToString()) || item.ToString().Contains(number.ToString()))
                    {
                        
                        var iteminfos= allItems.Where(m => m.UniqueDetail == item).FirstOrDefault();
                        var itemdetail=  foundItems.Where(m => m.ItemId == iteminfos.Id.ToString()).FirstOrDefault();
                        var reportedItems = await reportdbContext.ReportItems.ToListAsync();
                        var repo = reportedItems.Where(m => m.ItemId.ToString() == iteminfos.Id.ToString()).FirstOrDefault();
                        reportdbContext.ReportItems.Remove(repo);
                        if (itemdetail == null)
                        {
                            var ItemFound = new FoundItems()
                            {
                                Id = Guid.NewGuid(),
                                ItemFounderId = ItemFounderId,
                                UserItemId = iteminfos.UserId,
                                ItemId = iteminfos.Id.ToString(),
                            };
                            await dbContext.FoundItems.AddAsync(ItemFound);
                            await dbContext.SaveChangesAsync();
                            return Ok(ItemFound);
                        }
                        else if (itemdetail != null)
                        {
                            reportdbContext.ReportItems.Remove(repo);
                            return Ok(itemdetail);
                        }  
                    }
                }
                foreach (var item in desc)
                {
                    if (item.ToString().Contains(number2.ToString()) || item.ToString().Contains(number.ToString()))
                    {
                        
                        var iteminfos = allItems.Where(m => m.ItemDescription == item).FirstOrDefault();
                       
                        var itemdetail = foundItems.Where(m => m.ItemId == iteminfos.Id.ToString()).FirstOrDefault();
                        if(itemdetail == null)
                        {
                            var ItemFound = new FoundItems()
                            {
                                Id = Guid.NewGuid(),
                                ItemFounderId = ItemFounderId,
                                UserItemId = iteminfos.UserId,
                                ItemId = iteminfos.Id.ToString(),
                            };
                            await dbContext.FoundItems.AddAsync(ItemFound);
                            await dbContext.SaveChangesAsync();
                            return Ok(ItemFound);
                        }
                        else if (itemdetail != null)
                        {
                           
                            return Ok(itemdetail);
                        }

                    }
                    return Ok("not found");
                }
            }
            return Ok("done");

            
        }

       

    }
}
