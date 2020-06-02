namespace CatBreedsDetector.Web.Models
{
    using Common;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CatBreedDetectInputModel
    {
        [Required]
        [FileExtensions(Extensions = Constants.FileExtension.DefaultImageFileExtensions, ErrorMessage = Constants.Message.InvalidUploadedImage)]
        public IFormFile CatImage { get; set; }
    }
}
