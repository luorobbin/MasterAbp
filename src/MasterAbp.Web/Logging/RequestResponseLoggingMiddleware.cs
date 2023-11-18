﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MT.INV.Logging
 * 唯一标识：ce61dcf3-9d2a-42df-a359-4756afde9d41
 * 文件名：RequestResponseLoggingMiddleware
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/10/14 12:53:45
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>


using MasterAbp.Web.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAbp.WEb.Logging
{
    /// <summary>
    /// 请求与返回中间件
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _log;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> log)
        {
            _next = next;
            _log = log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            #region 这里可以加入正则验证context.Path。 过滤不需要记录日志的api

            var path = context.Request.Path.Value.ToString().ToLower();

            if (path.Contains("/index") || path.Contains("/check") ||
                path.Contains("/swagger") || path.Contains("/getsysdatas") || path.Contains("/load") || path.Contains("swashbuckle")
                || path == "/")
            {
                await CatchNext(context);
                return;
            }

            #endregion

            // 启用耗时 日志记录
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var logData = new Dictionary<string, object>();
            var request = context.Request;
            logData.Add("request.url", request.Path.ToString());
            logData.Add("request.headers",
                request.Headers.ToDictionary(x => x.Key, v => string.Join(";", v.Value.ToList())));
            logData.Add("request.method", request.Method);
            logData.Add("request.executeStartTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //追踪实别器
            logData.Add("traceIdentifier", context.TraceIdentifier);
            // 获取请求body内容
            if (request.Method.ToLower().Equals("post"))
            {
                // 启用倒带功能，就可以让 Request.Body 可以再次读取
                request.EnableBuffering();
                // 文件上传 记录文件信息
                if (path.Contains("/upload"))
                {
                    var content = string.Join(",", request.Form.Files.Select(item => item.FileName));
                    logData.Add("request.body", $"收到上传文件:{content}");
                }
                else
                {
                    var sr = new StreamReader(request.Body, Encoding.UTF8);
                    var content = sr.ReadToEndAsync().Result;
                    logData.Add("request.body", content);
                    request.Body.Position = 0;
                }
            }
            else if (request.Method.ToLower().Equals("get"))
            {
                logData.Add("request.body", request.QueryString.Value);
            }

            // 获取Response.Body内容
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await CatchNext(context);
                if (!logData.ContainsKey("response.body"))
                {
                    logData.Add("response.body", await GetResponse(context.Response));
                }

                logData.Add("response.executeEndTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                logData.Add("response.statusCode", context.Response.StatusCode);
                await responseBody.CopyToAsync(originalBodyStream);
            }

            // 响应完成记录时间和存入日志
            context.Response.OnCompleted(() =>
            {
                try
                {
                    stopwatch.Stop();
                    logData.Add("elapsedTime", stopwatch.ElapsedMilliseconds + "ms");

                    //var json = JsonHelper.Instance.Serialize(logData);                        
                    //_log.LogInformation(json);

                    return Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    return Task.FromException(ex);
                }
            });
        }

        private async Task CatchNext(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "系统日志错误,管道捕获");
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json; charset=utf-8";
                var result = new { code = 500, message = ex.Message ?? "系统日志错误,请稍后再试" };
                await context.Response.WriteAsync(JsonHelper.Instance.Serialize(result));
            }
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }

    }
}