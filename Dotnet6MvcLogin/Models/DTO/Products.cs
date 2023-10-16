using System.ComponentModel.DataAnnotations;

namespace Dotnet6MvcLogin.Models.DTO
{
    public class Products
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
    public class CreateProductViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
