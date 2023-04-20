namespace CatBreedsDetector.Server.Extensions;

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

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

        applicationBuilder.UseExceptionHandler(CustomExceptionHandler);
    }

    private static void CustomExceptionHandler(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Run(httpContext =>
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            return Task.CompletedTask;
        });
    }
}