using System.Reflection;
using Autofac;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PictureService.API;
using PictureService.ApiModels;
using PictureService.Application;
using PictureService.Data.MongoDB.Repositories;
using PictureService.Domain.Attributes.Interface;
using PictureService.Domain.Models;
using PictureService.Domain.MongoDbHelper;
using PictureService.Domain.Repositories.Pictures;
using PictureService.Middleware;

namespace PictureService;

/// <summary>
/// The Startup Class for the API
/// </summary>
public class Startup
{
    /// <summary>
    /// Holds the api configuration
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Configures the startup of the api
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Register dependencies with Autofac
    /// </summary>
    /// <param name="builder"></param>
    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<Bootstrap>();

        // Register IMongoRepository<> and MongoRepository<> with Autofac
        builder.RegisterType<MongoRepository<Picture>>().As<IMongoRepository<Picture>>().InstancePerLifetimeScope();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        //Configure AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        //Configure MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        //Configure Swagger
        services.AddSwagger();

        //Configure Add Mvc
        services.AddMvc().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });

        // Handle JSON serialization
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        //Configure Dapper
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        //Configure authorization
        /*services.AddAuthorization(configure =>
        {
        });*/

        // to solve chrome localhost error
        services.AddCors();

        //services.AddControllers();

        // Inject MongoDB settings and configuration
        services.Configure<MongoDBSettings>(Configuration.GetSection(nameof(MongoDBSettings)));
        services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value);

        // Inject MongoService
        services.AddMongoService();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(builder =>
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.SetIsOriginAllowed(x => true);
            builder.AllowCredentials();
        });

        app.UseMiddleware(typeof(ErrorHandling));

        app.UseSwagger();
        app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(Configuration["Swagger:Endpoint"], "Swagger API V1");
            }
        );

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}