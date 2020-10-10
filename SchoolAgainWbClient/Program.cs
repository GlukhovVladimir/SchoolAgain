using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainWbClient
{
    public class Program
    {
        public static ClientViewModel Client;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static bool AdminModel = false;
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
