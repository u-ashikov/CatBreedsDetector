namespace CatBreedsDetector.Web.Controllers
{
    using Classification.Interfaces;
    using Common;
    using Microsoft.AspNetCore.Http;
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

        public CatBreedsController(ICatBreedClassifier catBreedClassifier)
        {
            this.catBreedClassifier = catBreedClassifier;
        }

        [HttpPost]
        [Route("DetectAsync")]
        public async Task<IActionResult> DetectAsync([FromForm]CatBreedDetectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(Constants.Message.ProvideFileImage);
            }

            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var predictedImagesDirectoryPath = buildDir + $@"\{Constants.FilePath.PredictedImages}";
            var imagePath = predictedImagesDirectoryPath + $@"\{model.CatImage.FileName}";

            this.DeleteAlreadyPredictedImages(predictedImagesDirectoryPath);

            await this.SaveImageToFileAsync(imagePath, model.CatImage);

            var prediction = this.catBreedClassifier.ClassifySingleImage(imagePath);

            return this.Ok(prediction);
        }

        private void DeleteAlreadyPredictedImages(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                var predictedDirectory = new DirectoryInfo(directoryPath);

                foreach (var file in predictedDirectory.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        private async Task SaveImageToFileAsync(string imagePath, IFormFile imageFile)
        {
            using (var stream = System.IO.File.Create(imagePath))
            {
                await imageFile.CopyToAsync(stream);
            }
        }
    }
}
