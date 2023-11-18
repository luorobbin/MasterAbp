#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MT.INV.Logging
 * 唯一标识：455c37ce-7a86-49d7-85aa-24ce56349aef
 * 文件名：GlobalExceptionFilter
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/10/19 16:47:07
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Guids;
using Volo.Abp.Http;
using Volo.Abp.Json;

namespace MasterAbp.WEb.Logging
{
    /// <summary>
    /// 全局异常过滤
    /// </summary>
    public class GlobalExceptionFilter : IAsyncExceptionFilter, ITransientDependency
    {
        //private readonly ISystemExceptionLogRepository _systemExceptionLog;
        private readonly IGuidGenerator _guidGenerator;

        public GlobalExceptionFilter(
            //ISystemExceptionLogRepository systemExceptionLog,
            IGuidGenerator guidGenerator)
        {
            //_systemExceptionLog = systemExceptionLog;
            _guidGenerator = guidGenerator;
        }

        public virtual async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!ShouldHandleException(context))
            {
                LogException(context, out _);
                return;
            }

            await HandleAndWrapException(context);
        }

        protected virtual bool ShouldHandleException(ExceptionContext context)
        {
            if (context.ActionDescriptor.IsControllerAction() &&
                context.ActionDescriptor.HasObjectResult())
            {
                return true;
            }

            if (context.HttpContext.Request.CanAccept(MimeTypes.Application.Json))
            {
                return true;
            }

            if (context.HttpContext.Request.IsAjax())
            {
                return true;
            }

            return false;
        }

        protected virtual async Task HandleAndWrapException(ExceptionContext context)
        {
            LogException(context, out var remoteServiceErrorInfo);

            await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception));

            if (context.Exception is AbpAuthorizationException)
            {
                await context.HttpContext.RequestServices.GetRequiredService<IAbpAuthorizationExceptionHandler>()
                    .HandleAsync(context.Exception.As<AbpAuthorizationException>(), context.HttpContext);
            }
            else
            {
                context.HttpContext.Response.Headers.Add("_GlobalErrorFormat", "true");
                context.HttpContext.Response.StatusCode = (int)context
                    .GetRequiredService<IHttpExceptionStatusCodeFinder>()
                    .GetStatusCode(context.HttpContext, context.Exception);

                context.Result = new ObjectResult(new RemoteServiceErrorResponse(remoteServiceErrorInfo));
            }

            context.Exception = null; //Handled!
        }

        protected virtual void LogException(ExceptionContext context, out RemoteServiceErrorInfo remoteServiceErrorInfo)
        {
            var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
            var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
            remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
            {
                options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
                options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
            });

            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
            remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

            var logger = context.GetService<ILogger<GlobalExceptionFilter>>(NullLogger<GlobalExceptionFilter>.Instance);
            var logLevel = context.Exception.GetLogLevel();
            logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());
            logger.LogException(context.Exception, logLevel);

            Task.Run(async() =>
            {
                var httpContext = context.HttpContext;
                var exception = context.Exception;
                await WriteExceptionAsync(context.HttpContext, exception);
            }).Wait();
        }

        private async Task WriteExceptionAsync(HttpContext context, Exception ex)
        {
            var message = ex.Message;
            var request = context.Request;
            var path = request.Path.ToString();

            var requestStr = string.Empty;
            var responseStr = string.Empty;
            // 获取请求body内容
            if (request.Method.ToLower().Equals("post"))
            {
                // 启用倒带功能，就可以让 Request.Body 可以再次读取
                request.EnableBuffering();
                // 文件上传 记录文件信息
                if (path.Contains("/upload"))
                {
                    var content = string.Join(",", request.Form.Files.Select(item => item.FileName));
                    requestStr = content;
                }
                else
                {
                    request.Body.Position = 0;
                    var sr = new StreamReader(request.Body, Encoding.UTF8);
                    var content = sr.ReadToEndAsync().Result;
                    request.Body.Position = 0;
                    requestStr = content;
                }
            }
            else if (request.Method.ToLower().Equals("get"))
            {
                requestStr = request.QueryString.Value;
            }

            // 获取Response.Body内容
            responseStr = "异常捕获";
            var fullException = "异常堆栈:" + ex.StackTrace;
            var simpleException = message.Length > 2000 ? message?.Substring(0, 2000) : message;
            var ip = request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var dtNow = DateTime.Now;

            //await _systemExceptionLog.AddExceptionLog(new SystemExceptionLog(_guidGenerator.Create().ToString(), string.Empty, requestStr, responseStr, path, fullException, simpleException, ip, dtNow));
        }
    }
}