#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Controllers.Products
 * 唯一标识：c6bf79ff-0caf-4be4-ab11-afb3e80233c9
 * 文件名：ProductController
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/22 19:20:54
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

using MasterAbp.Products;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace MasterAbp.Controllers.Products
{
    /// <summary>
    /// ProductController 的摘要说明
    /// </summary>
    [Route("product2")]
    [Authorize]
    public class Product2Controller : MasterAbpController, IProductAppService
    {
        private readonly IProductAppService _productAppService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICurrentUser _currentUser;

        public Product2Controller(IProductAppService productAppService,
            IAuthorizationService authorizationService
            ,ICurrentUser currentUser)
        {
            _productAppService = productAppService;
            _authorizationService = authorizationService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize("ProductManagement.ProductCreation")]
        public async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
           return await _productAppService.CreateAsync(input);
        }

        //[Authorize("ProductManagement.ProductDeletion")]

        public async Task DeleteAsync(Guid id)
        {
            await _productAppService.DeleteAsync(id);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            return await _productAppService.GetAsync(id);
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
        {
            return await _productAppService.GetCategoriesAsync();
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var auth = HttpContext.AuthenticateAsync();
            var id = auth.Result.Principal.Claims.First(p => p.Type == "id").Value;
            var name = auth.Result.Principal.Claims.First(p => p.Type == "name").Value;
            return await _productAppService.GetListAsync(input);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            await _productAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 服务端流式返回
        /// </summary>
        /// <returns></returns>
        public async Task PostStreamText()
        {
            string filePath = "E:\\Robin\\netCore.txt";
            Response.ContentType = "application/octet-stream";
            var reader = new StreamReader(filePath);
            var buffer = new Memory<char>(new char[50]);
            int writeLength = 0;
            //每次读取50个字符写入到流中
            while ((writeLength = await reader.ReadBlockAsync(buffer)) > 0)
            {
                if (writeLength < buffer.Length)
                {
                    buffer = buffer[..writeLength];
                }
                await Response.WriteAsync(buffer.ToString());
                await Task.Delay(100);
            }
        }

        /// <summary>
        /// 客户端显示
        /// </summary>
        public async Task GetText()
        {
            var url = "https://localhost:44356/api/app/product2/streamText";
            var client = new HttpClient();
            using HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            var response = await client.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead);
            await using var stream = await response.Content.ReadAsStreamAsync();
            var streamReader = new StreamReader(stream);
            var buffer = new Memory<char>(new char[50]);
            int writeLength = 0;
            //while ((writeLength = await streamReader.ReadBlockAsync(buffer)) > 0)
            //{
            //    if (writeLength < buffer.Length)
            //    {
            //        buffer = buffer[..writeLength];
            //    }
            //    Debug.Write(buffer.ToString());
            //}
            var bytes = new byte[50];
            while ((writeLength = stream.Read(bytes, 0, bytes.Length)) > 0)
            {
                Debug.Write(Encoding.UTF8.GetString(bytes, 0, writeLength));
            }
            Debug.WriteLine("END");
        }

        /// <summary>
        /// 流式返回图片
        /// </summary>
        /// <returns></returns>
        public async Task GetImg()
        {
            string filePath = @"E:\CaiHua\图片/杂图2.jpg";
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out string contentType);
            Response.ContentType = contentType ?? "application/octet-stream";
            var fileStream = System.IO.File.OpenRead(filePath);
            var bytes = new byte[1024 * 1024];
            int writeLength = 0;
            while ((writeLength = fileStream.Read(bytes, 0, bytes.Length)) > 0)
            {
                await Response.Body.WriteAsync(bytes, 0, writeLength);
                await Task.Delay(500);
            }
        }
    }
}