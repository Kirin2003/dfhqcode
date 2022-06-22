using Autofac;
using AutoMapper;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BackendCode.AuthHelper;
using Achieve.Common;
using Achieve.Common.HttpContextUser;
using Achieve.Common.LogHelper;
using Achieve.Common.MemoryCache;
using BackendCode.Extensions;
using BackendCode.Filter;
using BackendCode.Log;
using BackendCode.Middlewares;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackendCode
{
    public class Startup
    {

        /// <summary>
        /// log4net �ִ���
        /// </summary>
        public static ILoggerRepository Repository { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            //log4net
            Repository = LogManager.CreateRepository(Configuration["Logging:Log4Net:Name"]);
            //ָ�������ļ�������������������⣬Ӧ����ʹ����InProcessģʽ����鿴Student.Achieve.csproj,��ɾ֮
            var contentPath = env.ContentRootPath;
            var log4Config = Path.Combine(contentPath, "log4net.config");
            XmlConfigurator.Configure(Repository, new FileInfo(log4Config));

        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        private const string ApiName = "Student.Achieve";
        private readonly string version = "V1";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region ���ַ���ע��-netcore�Դ�����
            // ����ע��
            services.AddScoped<ICaching, MemoryCaching>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            // log��־ע��
            services.AddSingleton<ILoggerHelper, LogHelper>();
            #endregion

            #region ��ʼ��DB
            services.AddScoped<Student.Achieve.Model.Models.DBSeed>();
            services.AddScoped<Student.Achieve.Model.Models.MyContext>();
            #endregion

            #region Automapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region CORS
            services.AddCors(c =>
            {
                //һ��������ַ���
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://127.0.0.1:6918", "http://localhost:6918")
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });

            #endregion

            #region Swagger UI Service

            var basePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = $"{ApiName} �ӿ��ĵ�����{RuntimeInformation.FrameworkDescription}",
                    Description = $"{ApiName} HTTP API " + version,
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName + " �ٷ��ĵ�", Url = new Uri("http://apk.neters.club/.doc/") }
                });
                c.OrderActionsBy(o => o.RelativePath);


                //��������
                var xmlPath = Path.Combine(basePath, "Student.Achieve.xml");
                c.IncludeXmlComments(xmlPath, true);

                var xmlModelPath = Path.Combine(basePath, "Student.Achieve.Model.xml");
                c.IncludeXmlComments(xmlModelPath);

                #region Token�󶨵�ConfigureServices


                // ������ȨС��
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // ��header�����token�����ݵ���̨
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // Jwt Bearer ��֤�������� oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });

            #endregion

            #region MVC + GlobalExceptions

            //ע��ȫ���쳣����
            services.AddControllers(o =>
            {
                // ȫ���쳣����
                o.Filters.Add(typeof(GlobalExceptionsFilter));
                // ȫ��·��Ȩ�޹�Լ
                //o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                // ȫ��·��ǰ׺��ͳһ�޸�·��
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
            //ȫ������Json���л�����
            .AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //��ʹ���շ���ʽ��key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
                //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });

            #endregion

            #region Httpcontext

            // Httpcontext ע��
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            #endregion

            #region Authorize Ȩ����֤������


            #region ����
            //��ȡ�����ļ�
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);


            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // ���Ҫ���ݿ⶯̬�󶨣������������գ���ߴ������ﶯ̬��ֵ
            var permission = new List<PermissionItem>();

            // ��ɫ��ӿڵ�Ȩ��Ҫ�����
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// �ܾ���Ȩ����ת��ַ��Ŀǰ���ã�
                permission,
                ClaimTypes.Role,//���ڽ�ɫ����Ȩ
                audienceConfig["Issuer"],//������
                audienceConfig["Audience"],//����
                signingCredentials,//ǩ��ƾ��
                expiration: TimeSpan.FromSeconds(60 * 60)//�ӿڵĹ���ʱ��
                );
            #endregion

            //����Ȩ��
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.Name,
                         policy => policy.Requirements.Add(permissionRequirement));
            });


            // ������֤����
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],//������
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],//������
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            services.AddAuthentication("Bearer")
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // ������ڣ����<�Ƿ����>��ӵ�������ͷ��Ϣ��
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             })
             .AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });


            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);

            #endregion

            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddSingleton(new LogLock(Env.ContentRootPath));



        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            #region ReuestResponseLog

            if (Appsettings.app("AppSettings", "Middleware_RequestResponse", "Enabled").ObjToBool())
            {
                app.UseReuestResponseLog();//��¼�����뷵������ 
            }

            #endregion

            #region Environment
            if (env.IsDevelopment())
            {
                // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Error");

            }
            #endregion

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Student.Achieve.index.html");
                c.RoutePrefix = "";
            });


            #endregion

            // CORS����
            app.UseCors("LimitRequests");
            // ��תhttps
            //app.UseHttpsRedirection();
            // ʹ�þ�̬�ļ�
            app.UseStaticFiles();
            // ʹ��cookie
            app.UseCookiePolicy();
            // ���ش�����
            app.UseStatusCodePages();
            // Routing
            app.UseRouting();
            // �����Զ�����Ȩ�м�������Գ��ԣ������Ƽ�
            // app.UseJwtTokenAuth();
            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization();
            // �����쳣�м����Ҫ�ŵ����
            //app.UseExceptionHandlerMidd();
            // ���ܷ���
            app.UseMiniProfiler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }

    }
}








/*using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
*/



