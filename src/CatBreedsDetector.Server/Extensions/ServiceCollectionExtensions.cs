namespace CatBreedsDetector.Web.Extensions
{
    using System;
    using System.IO;
    using System.Reflection;
    using CatBreedsDetector.Classification;
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Services.Contracts;
    using CatBreedsDetector.Services.Implementations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using NLog;

    /// <summary>
    /// A class containing extension methods over the <see cref="IServiceCollection"/> interface.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Use this method to register in the <see cref="IServiceCollection"/> all of the required custom services for the application to run properly.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> that should be used to register the dependencies.</param>
        /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection serviceCollection)
        {
            ArgumentNullException.ThrowIfNull(serviceCollection);

            serviceCollection.AddSingleton<ICatBreedClassifier, CatBreedClassifier>();
            serviceCollection.AddSingleton<IFileService, FileService>();

            return serviceCollection;
        }

        /// <summary>
        /// Use this method to configure the logging for the application.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> that should be used to register the required components.</param>
        /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureLogging(this IServiceCollection serviceCollection)
        {
            ArgumentNullException.ThrowIfNull(serviceCollection);

            serviceCollection.AddSingleton<ILogService, LogService>();
            serviceCollection.AddSingleton<ILogger>(_ => LogManager.GetLogger("FileLogger"));

            return serviceCollection;
        }

        /// <summary>
        /// Use this method to configure the Swagger middleware for serving the generated JSON document and the Swagger UI.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> that should be used to register the required components.</param>
        /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
        {
            ArgumentNullException.ThrowIfNull(serviceCollection);

            serviceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("Cat Breeds Detector v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Cat Breeds Detector API",
                    Description = "An ASP.NET Web API for detecting a cat's breed via ML.NET pre-trained model.",
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return serviceCollection;
        }
    }
}