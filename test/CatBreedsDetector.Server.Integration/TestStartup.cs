namespace CatBreedsDetector.Server.Integration
{
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Tests.Common.Mocks;
    using MyTested.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Web;

    public class TestStartup : Startup
    {
        /// <inheritdoc />
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.Replace<ICatBreedClassifier, CatBreedClassifierMock>(ServiceLifetime.Singleton);
        }
    }
}
