namespace CatBreedsDetector.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// A custom attribute that validates the allowed file extension.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CustomFileExtensionAttribute : ValidationAttribute
    {
        private string extensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFileExtensionAttribute"/> class.
        /// </summary>
        public CustomFileExtensionAttribute()
            : base(() => Constants.Message.InvalidUploadedFile)
        {
        }

        /// <summary>
        /// Gets or sets the allowed file extensions as a string.
        /// </summary>
        public string Extensions
        {
            get => string.IsNullOrEmpty(this.extensions) ? Constants.FileExtension.DefaultImageFileExtensions : this.extensions;
            set => this.extensions = value;
        }

        private string ExtensionsNormalized => this.extensions.Replace(Constants.StringSeparator.Space, string.Empty).ToUpperInvariant();

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var fileExtension = Path.GetExtension(((IFormFile)value).FileName.ToUpperInvariant());

            var isValid = this.ExtensionsNormalized
                .Split(new[] { Constants.StringSeparator.Comma }, StringSplitOptions.RemoveEmptyEntries)
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
