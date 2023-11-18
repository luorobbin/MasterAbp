#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MT.INV.Logging
 * 唯一标识：992ae267-bb0a-475c-868d-6d0604872c22
 * 文件名：ApplicationBuilderExtension
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/10/16 11:43:22
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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MasterAbp.Web.Logging
{
    /// <summary>
    /// ApplicationBuilderExtension
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 注入日志中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
         public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder, ExceptionHandlerOptions options)
            => builder.UseMiddleware<RequestResponseLoggingMiddleware>(Options.Create(options));

        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder, string errorHandlingPath)
            => builder.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandlingPath = new PathString(errorHandlingPath)
            });

        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return null;
            //return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public static IApplicationBuilder UseRealIpMiddleware(this IApplicationBuilder builder)
        {
            return null;
            //return builder.UseMiddleware<RealIpMiddleware>();
        }

    }

    public class ExceptionHandlerOptions
    {
        public RequestDelegate ExceptionHandler { get; set; }
        public PathString ExceptionHandlingPath { get; set; }
    }
}