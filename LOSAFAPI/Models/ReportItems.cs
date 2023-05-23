using System.ComponentModel.DataAnnotations.Schema;

namespace LOSAFAPI.Models
{
    public class ReportItems
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public string ItemId{ get; set; }

        public string image { get; set; }
        public string ItemCategory { get; set; }

        public string founderContact { get; set; }

        public string Email { get; set; }   

        public string founderName { get; set; }

        public string UniqueDetail { get; set; }

        public string ItemDescription { get; set; }

    }
}
