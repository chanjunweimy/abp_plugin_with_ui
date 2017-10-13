using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Todo.MainProject.AngularUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DownloadPlugins(args);

            BuildWebHost(args).Run();
        }

        private static void DownloadPlugins(string[] args)
        {
            var downloaderPath = "downloader/Todo.PluginDownloader.dll";
            if (!File.Exists(downloaderPath))
            {
                return;
            }

            var downloaderAssembly = Assembly.LoadFrom(Path.GetFullPath(downloaderPath));
            var type = downloaderAssembly.GetType("Todo.PluginDownloader.Program");
            var m = type.GetMethod("Execute");
            m.Invoke(null, new object[] {args});
        }

        public static IWebHost BuildWebHost(string[] args) {
            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("hosting.json", true)
                        .Build();
            var host = new WebHostBuilder()
                    .UseKestrel(options => options.AddServerHeader = false)
                    .UseConfiguration(configuration)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();

            return host;
        }
            
    }
}
