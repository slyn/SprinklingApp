using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SprinklingApp.Master.API.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isService = !(Debugger.IsAttached || args.Contains("--console"));

            if (isService)
            {
                string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                string pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            IWebHostBuilder builder = CreateWebHostBuilder(
                args.Where(arg => arg != "--console").ToArray());

            IWebHost host = builder.Build();

            if (isService)
            {
                // To run the app without the CustomWebHostService change the
                // next line to host.RunAsService();
                host.RunAsCustomService();
            }
            else
            {
                host.Run();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          //.ConfigureLogging((hostingContext, logging) => { logging.AddEventLog(); })
                          .ConfigureAppConfiguration(
                              (context, config) =>
                              {
                                  // Configure the app here.
                              })
                          .UseUrls(
                              //"http://localhost:9080"
                              "http://*:8080")
                          .UseStartup<Startup>();
        }
    }

}
