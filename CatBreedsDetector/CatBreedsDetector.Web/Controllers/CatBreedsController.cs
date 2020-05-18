namespace CatBreedsDetector.Web.Controllers
{
    using Classification.Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class CatBreedsController : ControllerBase
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        private readonly ICatBreedClassifier catBreedClassifier;

        public CatBreedsController(IWebHostEnvironment hostingEnvironment, ICatBreedClassifier catBreedClassifier)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.catBreedClassifier = catBreedClassifier;
        }

        [HttpPost]
        [Route("DetectAsync")]
        public async Task<IActionResult> DetectAsync([FromForm]CatBreedDetectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("You must provide a file!");
            }

            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = buildDir + $@"\PredictedImages\{model.CatImage.FileName}";

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.CatImage.CopyToAsync(stream);
            }

            var prediction = this.catBreedClassifier.ClassifySingleImage(path);

            return this.Ok(prediction);
        }
    }
}
