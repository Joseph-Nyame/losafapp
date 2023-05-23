namespace LOSAFAPI.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

public class AddItemsRegisteration
{
    public Guid Id { get; set; }

    public string UserId { get; set; }
    public string ItemName { get; set; }

    public string ItemCategory { get; set; }

    public string founderContact { get; set; }
    public string UniqueDetail { get; set; }

    public string ItemImage { get; set; } 
    
    public string ItemDescription { get; set; }

    [NotMapped]
    public IFormFile file { get; set; } 

   



}
