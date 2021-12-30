namespace CatBreedsDetector.Web.Controllers
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Common;
    using CatBreedsDetector.Web.Infrastructure.Helpers.Contracts;
    using CatBreedsDetector.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CatBreedsController : ControllerBase
    {
        private readonly ICatBreedClassifier _catBreedClassifier;

        private readonly IFileHelper _fileHelper;

        public CatBreedsController(ICatBreedClassifier catBreedClassifier, IFileHelper fileHelper)
        {
            this._catBreedClassifier = catBreedClassifier ?? throw new ArgumentNullException(nameof(catBreedClassifier));
            this._fileHelper = fileHelper ?? throw new ArgumentNullException(nameof(fileHelper));
        }

        [HttpPost(nameof(DetectAsync))]
        public async Task<IActionResult> DetectAsync([FromForm]CatBreedDetectInputModel model)
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var predictedImagesDirectoryPath = buildDir + $@"\{Constants.FilePath.PredictedImages}";
            var imagePath = predictedImagesDirectoryPath + $@"\{model.CatImage.FileName}";

            this._fileHelper.DeleteFilesInDirectory(predictedImagesDirectoryPath);

            await this._fileHelper.SaveImageToFileAsync(imagePath, model.CatImage);

            var prediction = this._catBreedClassifier.ClassifySingleImage(imagePath);

            var predictionResult = new CatBreedPredictionResultModel(prediction.PredictedLabelValue, prediction.PredictionProbability);

            return this.Ok(predictionResult);
        }
    }
}
