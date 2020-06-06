namespace CatBreedsDetector.Web.Infrastructure.Helpers.Contracts
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface IFileHelper
    {
        void DeleteFilesInDirectory(string directoryPath);

        Task SaveImageToFileAsync(string imagePath, IFormFile imageFile);
    }
}
