namespace CatBreedsDetector.Web.Models
{
    using Common;
    using Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CatBreedDetectInputModel
    {
        [Required]
        [CustomFileExtension(Extensions = Constants.FileExtension.DefaultImageFileExtensions, ErrorMessage = Constants.Message.InvalidUploadedFileExtension)]
        [FileSize(Constants.FileSize.MaxAllowedFileSizeInMb)]
        public IFormFile CatImage { get; set; }
    }
}
