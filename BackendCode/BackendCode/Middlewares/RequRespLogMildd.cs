﻿using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;
using Student.Achieve.Common.LogHelper;
using System.Text.RegularExpressions;

namespace Student.Achieve.Middlewares
{
    /// <summary>
    /// 中间件
    /// 记录请求和响应数据
    /// </summary>
    public class RequRespLogMildd
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public RequRespLogMildd(RequestDelegate next)
        {
            _next = next;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            // 过滤，只有接口
            if (context.Request.Path.Value.Contains("api"))
            {
                context.Request.EnableBuffering();
                Stream originalBody = context.Response.Body;

                try
                {
                    // 存储请求数据
                    RequestDataLog(context.Request);

                    using (var ms = new MemoryStream())
                    {
                        context.Response.Body = ms;

                        await _next(context);

                        // 存储响应数据
                        ResponseDataLog(context.Response, ms);

                        ms.Position = 0;
                        await ms.CopyToAsync(originalBody);
                    }
                }
                catch (Exception)
                {
                    // 记录异常
                    //ErrorLogData(context.Response, ex);
                }
                finally
                {
                    context.Response.Body = originalBody;
                }
            }
            else
            {
                await _next(context);
            }
        }

        private void RequestDataLog(HttpRequest request)
        {
            var sr = new StreamReader(request.Body);

            var content = $" QueryData:{request.Path + request.QueryString}\r\n BodyData:{sr.ReadToEnd()}";

            if (!string.IsNullOrEmpty(content))
            {
                Parallel.For(0, 1, e =>
                {
                    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Request Data:", content });

                });

                request.Body.Position = 0;
            }

        }


        private void ResponseDataLog(HttpResponse response, MemoryStream ms)
        {
            ms.Position = 0;
            var ResponseBody = new StreamReader(ms).ReadToEnd();

            // 去除 Html
            var reg = "<[^>]+>";
            var isHtml = Regex.IsMatch(ResponseBody, reg);

            if (!string.IsNullOrEmpty(ResponseBody))
            {
                Parallel.For(0, 1, e =>
                {
                    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Response Data:", ResponseBody });

                });
            }
        }

    }
}

