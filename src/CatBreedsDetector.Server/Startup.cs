namespace CatBreedsDetector.Server
{
    using CatBreedsDetector.Server.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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

            services
                .ConfigureCoreServices()
                .ConfigureLogging()
                .ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/CBD v1/swagger.json", "CBD v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.ConfigureExceptionHandler();
            app.UseRouting();

            app.UseCors(builder =>
            {
                builder
                    .WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseEndpoints(configure =>
            {
                configure.MapDefaultControllerRoute();
            });
        }
    }
}
