namespace CatBreedsDetector.Web.Infrastructure.Attributes
{
    using Common;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class FileExtensionsAttribute : ValidationAttribute
    {
        private string extensions;

        private string extensionsNormalized => this.extensions.Replace(Constants.StringSeparator.Space, string.Empty, StringComparison.Ordinal).ToUpperInvariant();

        public FileExtensionsAttribute()
            : base(() => Constants.Message.InvalidUploadedFile) { }

        public string Extensions
        {
            get => string.IsNullOrEmpty(this.extensions) ? Constants.FileExtension.DefaultImageFileExtensions : this.extensions;
            set => this.extensions = value;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return this.extensionsNormalized.Split(Constants.StringSeparator.Comma, StringSplitOptions.RemoveEmptyEntries)
                .Contains(Path.GetExtension(((IFormFile)value).FileName).ToUpperInvariant());
        }

        public override string FormatErrorMessage(string name) 
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, this.Extensions);
    }
}
