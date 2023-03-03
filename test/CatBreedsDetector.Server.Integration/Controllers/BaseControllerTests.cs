namespace CatBreedsDetector.Server.Integration.Controllers;

using System;
using System.Net.Http;
using Xunit;

public abstract class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly CustomWebApplicationFactory<Program> WebApplicationFactory;

    protected readonly HttpClient HttpClient;

    protected BaseControllerTests(CustomWebApplicationFactory<Program> webApplicationFactory)
    {
        this.WebApplicationFactory = webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));
        this.HttpClient = this.WebApplicationFactory.CreateClient();
    }
}