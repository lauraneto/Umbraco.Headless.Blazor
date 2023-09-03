using Swashbuckle.AspNetCore.SwaggerGen;
using Umbraco.Cms.Api.Common.DependencyInjection;
using Umbraco.Cms.Core;
using UmbracoDemo.CMS.Custom.Swagger;

namespace UmbracoDemo.CMS;

public class Startup
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup" /> class.
    /// </summary>
    /// <param name="webHostEnvironment">The web hosting environment.</param>
    /// <param name="config">The configuration.</param>
    /// <remarks>
    /// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337.
    /// </remarks>
    public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
    {
        _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <remarks>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
    /// </remarks>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddUmbraco(_env, _config)
            .AddBackOffice()
            .AddWebsite()
            .AddDeliveryApi()
            .AddComposers()
            .Build();

        // Set up CORS
        services.AddCors(options => options.AddDefaultPolicy(builder => builder
            .WithOrigins("https://localhost:44398", "https://localhost:5001")
            .WithHeaders("Preview", "Api-Key")));

        // Workaround for the fact that using System.Text.Json the type discriminator needs to be the first property!
        services.AddControllers().AddJsonOptions(Constants.JsonOptionsNames.DeliveryApi, options =>
        {
            options.JsonSerializerOptions.TypeInfoResolver = new CustomDeliveryApiJsonTypeResolver();
        });

        // Enable generation of typed content responses based on CMS content types
        services.Configure<SwaggerGenOptions>(options =>
        {
            options.SupportNonNullableReferenceTypes();

            // UseOneOfForPolymorphism is disabled as we are consuming the swagger with NSwag
            // If using other code generations tools, like Orval, it should be enabled for better compatibility
            //options.UseOneOfForPolymorphism();

            options.UseAllOfForInheritance();

            options.SchemaFilter<DeliveryApiContentTypesSchemaFilter>();
        });
    }

    /// <summary>
    /// Configures the application.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="env">The web hosting environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors();

        app.UseUmbraco()
            .WithMiddleware(u =>
            {
                u.UseBackOffice();
                u.UseWebsite();
            })
            .WithEndpoints(u =>
            {
                u.UseInstallerEndpoints();
                u.UseBackOfficeEndpoints();
                u.UseWebsiteEndpoints();
            });
    }
}