namespace CatBreedsDetector.Web.Infrastructure.Attributes
{
    using Common;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// This attribute validates the allowed file extensions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CustomFileExtensionAttribute : ValidationAttribute
    {
        private string extensions;

        private string extensionsNormalized => this.extensions.Replace(Constants.StringSeparator.Space, string.Empty, StringComparison.Ordinal).ToUpperInvariant();

        public CustomFileExtensionAttribute()
            : base(() => Constants.Message.InvalidUploadedFile) { }

        public string Extensions
        {
            get => string.IsNullOrEmpty(this.extensions) ? Constants.FileExtension.DefaultImageFileExtensions : this.extensions;
            set => this.extensions = value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var fileExtension = Path.GetExtension(((IFormFile)value).FileName.ToUpperInvariant());

            var isValid = this.extensionsNormalized
                .Split(Constants.StringSeparator.Comma, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => Constants.StringSeparator.Dot + e)
                .Contains(fileExtension);

            if (!isValid)
            {
                return new ValidationResult(Constants.Message.InvalidUploadedFileExtension);
            }

            return ValidationResult.Success;
        }
    }
}
