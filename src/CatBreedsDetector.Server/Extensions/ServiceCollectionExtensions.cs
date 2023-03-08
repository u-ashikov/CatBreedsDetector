namespace CatBreedsDetector.Web.Extensions
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.OpenApi.Models;
    using CatBreedsDetector.Classification;
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Services.Contracts;
    using CatBreedsDetector.Services.Implementations;
    using Microsoft.Extensions.DependencyInjection;
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
    }
}