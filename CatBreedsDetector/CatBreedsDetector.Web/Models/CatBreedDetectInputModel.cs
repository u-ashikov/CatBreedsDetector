namespace CatBreedsDetector.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using CatBreedsDetector.Common;
    using CatBreedsDetector.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    public class CatBreedDetectInputModel
    {
        [Required]
        [CustomFileExtension(Extensions = Constants.FileExtension.DefaultImageFileExtensions, ErrorMessage = Constants.Message.InvalidUploadedFileExtension)]
        [FileSize(Constants.FileSize.MaxAllowedFileSizeInMb)]
        public IFormFile CatImage { get; set; }
    }
}
