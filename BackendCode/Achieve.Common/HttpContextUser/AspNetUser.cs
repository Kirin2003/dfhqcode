using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Achieve.Common.Helper;
using Microsoft.AspNetCore.Http;

namespace Achieve.Common.HttpContextUser
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => GetName();

        private string GetName()
        {
            if (IsAuthenticated())
            {
                return _accessor.HttpContext.User.Identity.Name;
            }
            else
            {
                if (!string.IsNullOrEmpty(GetToken()))
                {
                    return GetUserInfoFromToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault().ObjToString();
                }
            }

            return "";
        }

        public int ID => GetClaimValueByType("jti").FirstOrDefault().ObjToInt();

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }


        /*一般来说，后台都会有接口校验设计，需要在请求头包含部分加密参数进行验证。

        我们的项目也是这样处理，外放的接口需要先调用登录接口获取Token，根据Token在请求头进行组合形成鉴权，才能成功通过后台的校验进行使用。

        这时候想，如果每个接口都需要提前先获取Token，工作量非常大，于是想到了类似Java的AOP的面向切面处理方式，使用动态代理灵活在接口方法前插入获取Token逻辑。

        而部分接口例如，获取验证码、登录相关的，并不需要Token，因此再自定义一个注解用于区别*/

        public string GetToken()
        {
            return _accessor.HttpContext.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
        }

        public List<string> GetUserInfoFromToken(string ClaimType)
        {

            var jwtHandler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(GetToken()))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(GetToken());

                return (from item in jwtToken.Claims
                        where item.Type == ClaimType
                        select item.Value).ToList();
            }
            else
            {
                return new List<string>() { };
            }
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public List<string> GetClaimValueByType(string ClaimType)
        {

            return (from item in GetClaimsIdentity()
                    where item.Type == ClaimType
                    select item.Value).ToList();

        }
    }
}
