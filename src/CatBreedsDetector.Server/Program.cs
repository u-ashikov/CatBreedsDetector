using System.Threading.Tasks;
using CatBreedsDetector.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateWebHostBuilder(args).Build();

        await host.RunAsync();
    }

    private static IHostBuilder CreateWebHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, builder) =>
            {
                builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                builder.AddDebug();
                builder.AddNLog();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}