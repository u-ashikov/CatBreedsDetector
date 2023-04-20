namespace CatBreedsDetector.Server;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using NLog.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        var startUp = new Startup(builder.Configuration);
        startUp.ConfigureServices(builder.Services);

        var app = builder.Build();
        startUp.Configure(app, builder.Environment);

        await app.RunAsync();
    }
}