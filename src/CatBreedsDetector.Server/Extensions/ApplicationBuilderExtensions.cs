namespace CatBreedsDetector.Server.Extensions
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;

    /// <summary>
    /// A static class containing extension methods over the <see cref="IApplicationBuilder"/> interface.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use this method to configure the global exception handling middleware.
        /// </summary>
        /// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/> that should be used during the configuration process.</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            ArgumentNullException.ThrowIfNull(applicationBuilder);

            applicationBuilder.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        // TODO: Log the error.
                    }
                });
            });
        }
    }
}