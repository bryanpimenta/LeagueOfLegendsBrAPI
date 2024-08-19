using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfLegendsBrAPI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string selectedDatabase = await DatabaseSelector.SelectDatabaseAsync();
            DockerComposeUpdater.UpdateDockerCompose(selectedDatabase);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {
                        options.ListenAnyIP(5050);
                    });
                });
    }
}
