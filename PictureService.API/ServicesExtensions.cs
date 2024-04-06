using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace PictureService.API;

public static class ServicesExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Picture Service API", Version = "v1" });

            var basePath = AppContext.BaseDirectory;
            var xmlPath = Path.Combine(basePath, "api.xml");
            x.IncludeXmlComments(xmlPath);
        });
    }
    public static IServiceCollection AddMongoService(this IServiceCollection services)
    {
        // Register GUID serializer
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        // Return the modified service collection
        return services;
    }
}