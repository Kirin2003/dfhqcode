using System;
using Achieve.Common.Helper;
//using Achieve.Model.EntityModels;
//using Achieve.Model.PermissionModels;
using Achieve.Model.Seed;
//using Achieve.Model.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace Student.Achieve
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ���ɳ��� web Ӧ�ó���� Microsoft.AspNetCore.Hosting.IWebHost��Build��WebHostBuilder���յ�Ŀ�ģ�������һ�������WebHost����������������
            var host = CreateWebHostBuilder(args).Build();

            // ���������ڽ��������������� Microsoft.Extensions.DependencyInjection.IServiceScope��
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    // �� system.IServicec�ṩ�����ȡ T ���͵ķ���
                    // Ϊ�˴�ҵ����ݰ�ȫ��������ע�͵��ˣ�����Լ��Ȳ�����һ��ɡ�
                    // ���ݿ������ַ������� Model ��� Seed �ļ����µ� MyContext.cs ��
                    var configuration = services.GetRequiredService<IConfiguration>();
                    if (configuration.GetSection("AppSettings")["SeedDBEnabled"].ObjToBool())
                    {
                        var myContext = services.GetRequiredService<MyContext>();
                        DBSeed.SeedAsync(myContext).Wait();
                    }
                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "Error occured seeding the Database.");
                    throw;
                }
            }

            // ���� web Ӧ�ó�����ֹ�����߳�, ֱ�������رա�
            // ������ WebHost ֮�󣬱�������� Run �������� Run ������ȥ���� WebHost �� StartAsync ����
            // ��Initialize����������Application�ܵ������Թ�������Ϣ
            // ִ��HostedServiceExecutor.StartAsync����
            host.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            //ʹ��Ԥ���õ�Ĭ��ֵ��ʼ�� Microsoft.AspNetCore.Hosting.WebHostBuilder �����ʵ����
            Host.CreateDefaultBuilder(args)
             //ָ��Ҫ�� web ����ʹ�õ��������͡��൱��ע����һ��IStartup���񡣿����Զ����������񣬱���.UseStartup(typeof(StartupDevelopment).GetTypeInfo().Assembly.FullName)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder
                 .UseStartup<WebHostBuilder>()
                 .UseUrls("http://*:691");
             });
            /*{
                 webBuilder
                 .UseStartup<Startup>()
                 .UseUrls("http://*:691");
            });
            �������һ��*/
    }
}










/*using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
*/



