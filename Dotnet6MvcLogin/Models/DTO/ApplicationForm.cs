using System.ComponentModel.DataAnnotations;

namespace Dotnet6MvcLogin.Models.DTO
{
    public class ApplicationForm
    {
       
            [Key]
            public int ApplicationNumber { get; set; }
            public string UserId { get; set; }

         
            public string FirstName { get; set; }
          
            public string LastName { get; set; }
            public DateTime SubmittedDate { get; set; }
        public DateTime EndDate { get; set; }
        public int productId { get;set; } 
        public decimal productPrice { get; set; }       //Bidder will bid this price
            public string Status { get; set; } // New Status property to track verification status
        


    }
}
