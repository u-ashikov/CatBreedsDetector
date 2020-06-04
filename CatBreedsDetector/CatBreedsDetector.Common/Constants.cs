namespace CatBreedsDetector.Common
{
    public class Constants
    {
        public class Message
        {
            public const string MissingOrInvalidFile = "File is missing or invalid!";

            public const string InvalidUploadedFile = "Invalid file!";

            public const string InvalidUploadedFileExtension = "The uploaded file is not allowed. The allowed file extensions are: png, jpeg, jpg, gif.";
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
    }
}
