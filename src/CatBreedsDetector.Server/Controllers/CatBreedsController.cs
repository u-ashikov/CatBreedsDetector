namespace CatBreedsDetector.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Common;
    using CatBreedsDetector.Models;
    using CatBreedsDetector.Models.Models;
    using CatBreedsDetector.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CatBreedsController : ControllerBase
    {
        private const string DirectoryPath = "C:\\Temp\\TestFiles";

        private readonly ICatBreedClassifier _catBreedClassifier;

        private readonly IFileService _fileService;

        public CatBreedsController(ICatBreedClassifier catBreedClassifier, IFileService fileService)
        {
            this._catBreedClassifier = catBreedClassifier ?? throw new ArgumentNullException(nameof(catBreedClassifier));
            this._fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        [HttpPost(nameof(DetectAsync))]
        public async Task<IActionResult> DetectAsync([FromForm] CatBreedDetectInputModel model)
        {
            if (model is null || !this.ModelState.IsValid)
                return this.BadRequest();

            var predictedImagesDirectoryPath = DirectoryPath + $@"\{Constants.FilePath.PredictedImages}";
            var imagePath = predictedImagesDirectoryPath + $@"\{model.CatImage.FileName}";

            this._fileService.DeleteFilesInDirectory(predictedImagesDirectoryPath);

            await this._fileService.SaveImageToFileAsync(imagePath, model.CatImage);

            var prediction = this._catBreedClassifier.ClassifySingleImage(imagePath);

            var predictionResult = new CatBreedPredictionResultModel(prediction.PredictedLabelValue, prediction.PredictionProbability);

            return this.Ok(predictionResult);
        }
    }
}
