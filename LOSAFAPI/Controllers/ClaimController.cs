using LOSAFAPI.Data;
using LOSAFAPI.Migrations.FoundItemsDb;
using LOSAFAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LOSAFAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ClaimController : Controller
    {
        private readonly FoundItemsDbContext dbContext;
        private readonly AddRegisterItemsDbContext itemsContext;
        //private readonly JwtAuthenticationManager key;
        private readonly ReportItemsDbContext reportdbContext;
        private readonly FoundItemsDbContext foundItemsContext;
        private readonly UserApiDbContext userContext;


        public ClaimController(FoundItemsDbContext dbContext, AddRegisterItemsDbContext itemContext, ReportItemsDbContext reportdbContext, UserApiDbContext userContext,FoundItemsDbContext foundItemContext)
        {
            this.dbContext = dbContext;
            this.itemsContext = itemContext;
            this.reportdbContext = reportdbContext;
            this.userContext = userContext;
            this.foundItemsContext = foundItemContext;
        }
       
        [HttpPost]
        public async Task<IActionResult> ClaimItem(ClaimItem claimItem)
        {
            var itemFounderId = claimItem.ItemFounderId;

            var users = await userContext.Users.ToListAsync();
            //var founderdetail = 
            var allitems = await reportdbContext.ReportItems.ToListAsync();
            var allregistereditems = await itemsContext.ItemsRegisteration.ToListAsync();
            var FounderInfo = users.Where(m => m.Id.ToString() == itemFounderId).FirstOrDefault();
            var itemandfounderdetail = allregistereditems.Where(i => i.founderContact == FounderInfo.Id.ToString()).FirstOrDefault();
            var founderdet = itemandfounderdetail.founderContact;
            var founderdetail = users.Where(m => m.Id.ToString() == founderdet).FirstOrDefault();
            // var itemfounderInfo = allitems.Where(x => x.Email == Info.Email).FirstOrDefault();
            // var info = myItemInfo.Id;

            //List<Object> items = new List<Object>();
            //foreach (var item in foundItems)
            //{
            //    var itemdetails = await itemsContext.ItemsRegisteration.SingleOrDefaultAsync(x => x.Id.ToString() == item.ItemId.ToString());


            //    items.Add(itemdetails);


            //}

            // var foundersID = await foundItemsContext.FoundItems.ToListAsync();
            // List<Object> finder = new List<Object>();
            //var ase = foundersID.Where(m => m.UserItemId == info.ToString());
            // foreach (var a in ase)
            // {
            //     var find = users.Where(m => m.Id.ToString() == a.ItemFounderId);
            //     finder.Add(find);

            // }


            // var tuple = Tuple.Create(myItemInfo, finder);

            // return the tuple in an OK response
            // return Ok(tuple);

            return Ok(founderdetail);




        }
    }
}
