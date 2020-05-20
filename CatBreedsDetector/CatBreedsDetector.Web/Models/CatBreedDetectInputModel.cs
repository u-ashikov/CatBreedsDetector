namespace CatBreedsDetector.Web.Models
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CatBreedDetectInputModel
    {
        [Required]
        public IFormFile CatImage { get; set; }
    }
}
