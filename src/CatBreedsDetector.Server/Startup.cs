namespace CatBreedsDetector.Web
{
    using CatBreedsDetector.Classification;
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Services.Contracts;
    using CatBreedsDetector.Services.Implementations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NLog;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                    {
                        options.SuppressInferBindingSourcesForParameters = true;
                        options.InvalidModelStateResponseFactory = actionContext =>
                        {
                            var errors = actionContext.ModelState.Values;
                            return new BadRequestObjectResult(errors);
                        };
                    });

            services.AddSingleton<ICatBreedClassifier, CatBreedClassifier>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<ILogger>(_ => LogManager.GetLogger("FileLogger"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
