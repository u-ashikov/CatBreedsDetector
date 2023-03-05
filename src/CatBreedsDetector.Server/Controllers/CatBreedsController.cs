namespace CatBreedsDetector.Web.Controllers
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Common;
    using CatBreedsDetector.Models.InputModels;
    using CatBreedsDetector.Models.ViewModels;
    using CatBreedsDetector.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CatBreedsController : ControllerBase
    {
        private readonly ICatBreedClassifier _catBreedClassifier;

        private readonly IFileService _fileService;

        public CatBreedsController(ICatBreedClassifier catBreedClassifier, IFileService fileService)
        {
            this._catBreedClassifier = catBreedClassifier ?? throw new ArgumentNullException(nameof(catBreedClassifier));
            this._fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DetectAsync([FromForm] CatBreedDetectInputModel model, CancellationToken cancellationToken)
        {
            if (model is null || !this.ModelState.IsValid)
                return this.BadRequest();

            var rootPredictedImagesDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var predictedImagesDirectoryPath = Path.Combine(rootPredictedImagesDirectory, Constants.FilePath.PredictedImages);
            var imagePath = Path.Combine(predictedImagesDirectoryPath, model.CatImage.FileName);

            var deleteFilesInDirectory = this._fileService.DeleteFilesInDirectory(predictedImagesDirectoryPath);
            if (!deleteFilesInDirectory.IsSuccessful)
                return this.BadRequest(Constants.Message.UnsuccessfulOperation);

            var saveImageToFile = await this._fileService.SaveImageToFileAsync(imagePath, model.CatImage, cancellationToken);
            if (!saveImageToFile.IsSuccessful)
                return this.BadRequest(saveImageToFile.Errors);

            var classifyImage = this._catBreedClassifier.ClassifySingleImage(imagePath);
            if (!classifyImage.IsSuccessful)
                return this.BadRequest(classifyImage.Errors);

            var prediction = classifyImage.Outcome;
            var predictionResult = new CatBreedPredictionResultModel(prediction.PredictedLabelValue, prediction.PredictionProbability);

            return this.Ok(predictionResult);
        }
    }
}
