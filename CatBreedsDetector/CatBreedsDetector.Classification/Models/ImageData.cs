namespace CatBreedsDetector.Classification.Models
{
    using Microsoft.ML.Data;

    /// <summary>
    /// A class representing a classification mapping model.
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// Gets or sets a value of the image path.
        /// </summary>
        [LoadColumn(0)]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets a value of the image label.
        /// </summary>
        [LoadColumn(1)]
        public string Label { get; set; }
    }
}
