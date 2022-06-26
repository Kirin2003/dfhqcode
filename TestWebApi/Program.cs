using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Services.utils;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using BackendCode.Base;
using MySql.Data.MySqlClient;

namespace TestWebApi
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            MySqlDB mysql = new MySqlDB();
            mysql.Add("");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
