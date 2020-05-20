namespace CatBreedsDetector.Web.Controllers
{
    using Classification.Interfaces;
    using Common;
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
            var path = buildDir + $@"\{Constants.FilePath.PredictedImages}\{model.CatImage.FileName}";

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.CatImage.CopyToAsync(stream);
            }

            var prediction = this.catBreedClassifier.ClassifySingleImage(path);

            return this.Ok(prediction);
        }
    }
}
