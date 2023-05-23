using System.ComponentModel.DataAnnotations.Schema;

namespace LOSAFAPI.Models
{
    public class LogItem
    {
        public string UserId { get; set; }
        public string ItemName { get; set; }

        public string ItemCategory { get; set; }

        public string UniqueDetail { get; set; }

        public string ItemImage { get; set; }

        public string ItemDescription { get; set; }

        public string ItemFounderId { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }
    }
}
