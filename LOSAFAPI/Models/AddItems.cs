using System.ComponentModel.DataAnnotations.Schema;

namespace LOSAFAPI.Models

{
    public class AddItems
    {
        public string email { get; set; }  
        public string UserId { get; set; }
        public string ItemName { get; set; }

        public string ItemCategory { get; set; }

        public string UniqueDetail { get; set; }


        public string ItemImage { get; set; }

        public string ItemDescription { get; set; }

        [NotMapped]
        public IFormFile file{ get; set; }


    }
}
