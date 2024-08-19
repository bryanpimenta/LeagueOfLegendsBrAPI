using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dotenv.Net;

namespace LeagueOfLegendsBrAPI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Dotenv.Load();
            string selectedDatabase = await DatabaseSelector.SelectDatabaseAsync();
            DockerComposeUpdater.UpdateDockerCompose(selectedDatabase);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
