namespace CatBreedsDetector.Web.Controllers
{
    using Classification.Interfaces;
    using Common;
    using Infrastructure.Helpers.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class CatBreedsController : ControllerBase
    {
        private readonly ICatBreedClassifier catBreedClassifier;

        private readonly IFileHelper fileHelper;

        public CatBreedsController(ICatBreedClassifier catBreedClassifier, IFileHelper fileHelper)
        {
            this.catBreedClassifier = catBreedClassifier;
            this.fileHelper = fileHelper;
        }

        [HttpPost]
        [Route("DetectAsync")]
        public async Task<IActionResult> DetectAsync([FromForm]CatBreedDetectInputModel model)
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var predictedImagesDirectoryPath = buildDir + $@"\{Constants.FilePath.PredictedImages}";
            var imagePath = predictedImagesDirectoryPath + $@"\{model.CatImage.FileName}";

            this.fileHelper.DeleteFilesInDirectory(predictedImagesDirectoryPath);

            await this.fileHelper.SaveImageToFileAsync(imagePath, model.CatImage);

            var prediction = this.catBreedClassifier.ClassifySingleImage(imagePath);

            var predictionResult = new CatBreedPredictionResultModel(prediction.PredictedLabelValue, prediction.PredictionProbability);

            return this.Ok(predictionResult);
        }
    }
}
