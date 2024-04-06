using Autofac;
using FluentValidation;
using MediatR;
using PictureService.Domain;
using Microsoft.Extensions.Configuration;
using PictureService.Application.Infrastructure;
using PictureService.Data.Dapper;
using PictureService.Data.Dapper.Repositories;

namespace PictureService.Application;

public class Bootstrap : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterDataLayer(builder);
        RegisterMediatR(builder);
        RegisterServiceLayer(builder);
    }

    private void RegisterDataLayer(ContainerBuilder builder)
    {
        builder.Register(c =>
                new ConnectionString(c.Resolve<IConfiguration>().GetConnectionString("SQLConnection")!))
            .As<IConnectionString>();

        builder.RegisterType<UnitOfWork>()
            .AsSelf()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>()
            .AsSelf()
            .As<ISql>()
            .InstancePerLifetimeScope();

        // Register all data store and data reader types
        builder.RegisterAssemblyTypes(typeof(BaseReader<>).Assembly)
            .Where(x => x.Namespace != null && x.Namespace.Contains("Repositories"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
            .AsImplementedInterfaces();

        var mediatrOpenTypes = new[]
        {
            typeof(IRequestHandler<>),
            typeof(IRequestHandler<,>),
            typeof(INotificationHandler<>),
            typeof(IValidator<>)
        };

        // Find command and query handlers in this assembly and register them
        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
        }

        builder.RegisterGeneric(typeof(CommandPipeline<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(QueryPipeline<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerLifetimeScope();
    }

    private void RegisterServiceLayer(ContainerBuilder builder)
    {
        /*
        builder.RegisterAssemblyTypes(typeof(PrintService).Assembly)
            .Where(x => x.Namespace != null && x.Namespace.Contains("Services"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope().PreserveExistingDefaults();
        */
    }
}