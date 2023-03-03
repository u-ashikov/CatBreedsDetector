namespace CatBreedsDetector.Server.Integration;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CatBreedsDetector.Classification.Interfaces;
using CatBreedsDetector.Tests.Common.Mocks;
using CatBreedsDetector.Services.Contracts;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    /// <inheritdoc />
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Replace(ServiceDescriptor.Singleton<ICatBreedClassifier, CatBreedClassifierMock>());
            services.Replace(ServiceDescriptor.Singleton<IFileService, FileServiceMock>());
        });
        
        builder.UseEnvironment("Development");
    }
}