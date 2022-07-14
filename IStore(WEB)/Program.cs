using Business.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Threading.Tasks;

namespace IStore_WEB_
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
             await BusinessConfiguration.ConfigureIdentityInicializerAsync(host.Services.CreateScope().ServiceProvider);
             host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo
                .RollingFile("Logs/log-{Date}.txt", LogEventLevel.Debug).WriteTo.Seq("http://localhost:5341")
                .CreateLogger();


            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>().UseSerilog(); });
        }


        //http://localhost:5341/
    }
}