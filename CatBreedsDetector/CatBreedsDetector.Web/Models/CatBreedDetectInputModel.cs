namespace CatBreedsDetector.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CatBreedDetectInputModel
    {
        [Required]
        public IFormFile CatImage { get; set; }
    }
}
