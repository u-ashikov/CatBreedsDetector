namespace CatBreedsDetector.Common;

public class Constants
{
    public class Message
    {
        public const string InvalidUploadedFile = "Invalid file!";

        public const string InvalidUploadedFileExtension = "The uploaded file is not allowed. The allowed file extensions are: png, jpeg, jpg, gif.";

        public const string InvalidFileSize = "The uploaded file size is invalid!";

        public const string FileSizeCannotBeNegative = "The {0} file size cannot be negative value.";

        public const string MinSizeGreaterThanMaxSize = "The min file size cannot be greater than max file size.";

        public const string MaxFileSize = "The uploaded file size must be at most {0} megabytes.";

        public const string DirectoryDoesNotExist = "The provided directory does not exist.";

        public const string InvalidImagePath = "The provided image path is invalid.";

        public const string InvalidImageFile = "The provided image file is invalid.";

        public const string UnsuccessfulOperation = "The operation was not successful. Please, try again or reach out to your administrator.";

        public const string ErrorsCollectionIsNotValid = "The provided errors collection is not a valid one.";
    }

    public class FilePath
    {
        public const string PredictedImages = "PredictedImages";
    }

    public class FileExtension
    {
        public const string DefaultImageFileExtensions = "png, jpeg, jpg, gif";
    }

    public class StringSeparator
    {
        public const string Space = " ";

        public const string Comma = ",";

        public const string Dot = ".";
    }

    public class FileSize
    {
        public const int MbSize = 1024;

        public const int MaxAllowedFileSizeInMb = 2;
    }
}