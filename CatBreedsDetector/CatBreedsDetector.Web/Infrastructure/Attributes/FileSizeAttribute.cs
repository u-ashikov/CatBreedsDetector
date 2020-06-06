namespace CatBreedsDetector.Web.Infrastructure.Attributes
{
    using Common;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This attribute validates the allowed file size.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class FileSizeAttribute : ValidationAttribute
    {
        public long MinFileSize { get; set; }

        public long MaxFileSize { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileSizeAttribute" /> class.
        /// </summary>
        /// <param name="maxFileSize">
        ///     The maximum allowable length of a file in megabytes.
        ///     Value must be greater than or equal to zero.
        /// </param>
        public FileSizeAttribute(long maxFileSize)
            :base(Constants.Message.InvalidFileSize)
        {
            MaxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            this.ValidateFileSizes();

            var lengthInMb = ((IFormFile) value).Length / (Constants.FileSize.MbSize * Constants.FileSize.MbSize);

            var isValid = lengthInMb >= this.MinFileSize && lengthInMb <= this.MaxFileSize;

            if (!isValid)
            {
                return new ValidationResult(string.Format(Constants.Message.MaxFileSize, this.MaxFileSize));
            }

            return ValidationResult.Success;
        }

        private void ValidateFileSizes()
        {
            if (this.MinFileSize < default(long))
            {
                throw new InvalidOperationException(string.Format(Constants.Message.FileSizeCannotBeNegative, nameof(this.MinFileSize)));
            }

            if (this.MaxFileSize < default(long))
            {
                throw new InvalidOperationException(string.Format(Constants.Message.FileSizeCannotBeNegative, nameof(this.MaxFileSize)));
            }

            if (this.MinFileSize > this.MaxFileSize)
            {
                throw new InvalidOperationException(Constants.Message.MinSizeGreaterThanMaxSize);
            }
        }
    }
}
