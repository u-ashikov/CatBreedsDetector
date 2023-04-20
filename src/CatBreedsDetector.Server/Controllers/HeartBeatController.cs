namespace CatBreedsDetector.Server.Controllers;

using CatBreedsDetector.Server.Common;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HeartBeatController : ControllerBase
{
    /// <summary>
    /// Use this endpoint to check if the API server is up and running.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing information whether the API server is up and running.</returns>
    [HttpGet]
    public IActionResult Get() => this.Ok(ServerConstants.Messages.ServerIsRunning);
}