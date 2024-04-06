using Autofac.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace PictureService;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging(logging => {
                logging.AddNLog();
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
}