﻿using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SprinklingApp.Master.API {

    public class Program {
        //public static void Main(string[] args) {
        //    CreateWebHostBuilder(args).Build().Run();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
        //    return WebHost.CreateDefaultBuilder(args)
        //                  .UseStartup<Startup>();
        //}

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
                              "http://*:80")
                          .UseStartup<Startup>();
        }
    }

    public static class WebHostServiceExtensions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new CustomWebHostService(host);
            ServiceBase.Run(webHostService);
        }
    }

    internal class CustomWebHostService : WebHostService
    {
        private readonly ILogger logger;

        public CustomWebHostService(IWebHost host) : base(host)
        {
            logger = host.Services.GetRequiredService<ILogger<CustomWebHostService>>();
        }

        protected override void OnStarting(string[] args)
        {
            logger.LogInformation("OnStarting method called.");
            base.OnStarting(args);
        }

        protected override void OnStarted()
        {
            logger.LogInformation("OnStarted method called.");
            base.OnStarted();
        }

        protected override void OnStopping()
        {
            logger.LogInformation("OnStopping method called.");
            base.OnStopping();
        }
    }

}