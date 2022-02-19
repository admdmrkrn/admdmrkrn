using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using webapi.DBOperetions;

namespace webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host= CreateHostBuilder(args).Build();
           using(var scope=host.Services.CreateScope())
           {
               var Services=scope.ServiceProvider; 
               DataGenerator.Initailize(Services);
           }
           host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
