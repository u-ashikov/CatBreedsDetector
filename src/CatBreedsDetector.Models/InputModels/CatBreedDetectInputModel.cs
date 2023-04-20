namespace CatBreedsDetector.Models.InputModels;

using CatBreedsDetector.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using CatBreedsDetector.Common;
using Microsoft.AspNetCore.Http;
    
/// <summary>
/// A class representing an input model for a cat breed detection.
/// </summary>
public class CatBreedDetectInputModel
{
    /// <summary>
    /// Gets or sets the image file of a cat.
    /// </summary>
    [Required]
    [CustomFileExtension(Extensions = Constants.FileExtension.DefaultImageFileExtensions, ErrorMessage = Constants.Message.InvalidUploadedFileExtension)]
    [FileSize(Constants.FileSize.MaxAllowedFileSizeInMb)]
    public IFormFile CatImage { get; set; }
}