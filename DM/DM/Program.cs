using System;
using System.Net.Http;
using System.Threading.Tasks;
using DM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Threading;
using offline_module.Domain.Interfaces;
using offline_module.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Start the web host in a separate thread
            var hostThread = new Thread(() => CreateHostBuilder(args).Build().Run());
            hostThread.Start();

            // Start the business logic to make periodic HTTP requests
           /* var businessLogicThread = new Thread(RunBusinessLogic);
            businessLogicThread.Start();

            // Optionally, wait for the web host to finish running
            hostThread.Join();*/
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                // TODO: hardcode
            });
    }
}
