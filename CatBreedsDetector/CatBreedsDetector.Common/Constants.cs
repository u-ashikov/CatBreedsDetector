namespace CatBreedsDetector.Common
{
    public class Constants
    {
        public class Message
        {
            public const string ProvideFileImage = "You must provide a file!";

            public const string MissingOrInvalidFile = "File is missing or invalid!";

            public const string InvalidUploadedFile = "Invalid file!";

            public const string InvalidUploadedImage = "The uploaded image is not allowed. The allowed image extensions are: png, jpeg, jpg, gif.";
        }

        public class FilePath
        {
            public const string PredictedImages = "PredictedImages";
        }

        public class FileExtension
        {
            public const string DefaultImageFileExtensions = "png,jpeg,jpg,gif";
        }

        public class StringSeparator
        {
            public const string Space = " ";

            public const string Comma = ",";
        }
    }
}
