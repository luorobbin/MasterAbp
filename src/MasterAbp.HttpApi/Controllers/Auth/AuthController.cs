#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Controllers.Auth
 * 唯一标识：918b322b-bd89-46ed-9740-6db130bb0c23
 * 文件名：Auth
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/5/23 9:12:32
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

using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MasterAbp.Controllers.Auth
{
    /// <summary>
    /// Auth 的摘要说明
    /// </summary>
    [Route("auth2")]
    public class AuthController : MasterAbpController
    {
        [HttpGet]
        public IActionResult GetToken()
        {
            try
            {
                //定义发行人issuer
                string iss = "JWTBearer.Auth";
                //定义受众人audience
                string aud = "api.auth";

                //定义许多种的声明Claim,信息存储部分,Claims的实体一般包含用户和一些元数据
                IEnumerable<Claim> claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Id,"1"),
                    new Claim(JwtClaimTypes.Name,"i3yuan"),
                };
                //notBefore  生效时间
                // long nbf =new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
                var nbf = DateTime.UtcNow;
                //expires   //过期时间
                // long Exp = new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds();
                var Exp = DateTime.UtcNow.AddSeconds(1000);
                //signingCredentials  签名凭证
                string sign = "q2xiARx$4x3TKqBJmasterapbtestluorobbin"; //SecurityKey 的长度必须 大于等于 16个字符
                var secret = Encoding.UTF8.GetBytes(sign);
                var key = new SymmetricSecurityKey(secret);
                var signcreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwt = new JwtSecurityToken(issuer: iss, audience: aud, claims: claims, notBefore: nbf, expires: Exp, signingCredentials: signcreds);
                var JwtHander = new JwtSecurityTokenHandler();
                var token = JwtHander.WriteToken(jwt);
                return Ok(new
                {
                    access_token = token,
                    token_type = "Bearer",
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}