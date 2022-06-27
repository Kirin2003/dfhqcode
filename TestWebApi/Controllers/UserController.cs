using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Services;
using BackendCode.Models;
using TestWebApi.Controllers.utils;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        // POST: api/user/register
        // 用户注册
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {
            // 用户密码加密
            user.Password = MD5Helper.MD5Pwd(user.Password);

            bool result = userService.StoreUserInfo(user);

            if (result)
            {
                return Ok(user.Id);
            }
            else
            {
                return BadRequest("注册失败，请重新注册");
            }
        }

        // POST: api/user/login?name= && password= 
        // 用户注册
        [HttpPost("login")]
        public IActionResult Login([FromQuery]string name, string password)
        {
            // 密码加密
            password = MD5Helper.MD5Pwd(password);

            bool result = userService.QueryUser(name, password);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/user/delete/{id}
        // 用户注销
        [HttpDelete("{id}")]
        public IActionResult Logout(long id)
        {
            var result = userService.DeleteUserInfo(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
