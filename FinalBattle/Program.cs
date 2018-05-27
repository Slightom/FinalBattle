using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FinalBattle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
            .UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, 443, listenOptions =>
                {
                    listenOptions.UseHttps("server.pfx", "password");
                });
            })
            .UseStartup<Startup>()
            .Build();
        //WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .Build();
        //      WebHost.CreateDefaultBuilder(args)
        //.UseKestrel()
        //.UseUrls("http://*:80")
        //.UseContentRoot(Directory.GetCurrentDirectory())
        //              .UseStartup<Startup>()
        //              .Build();
    }
}
