﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Achieve.IRepository;
using Student.Achieve.Model;
using Student.Achieve.Model.Models;
using System.Threading.Tasks;

namespace Student.Achieve.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class RoleController : ControllerBase
    {
        readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleRepository"></param>
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<MessageModel<PageModel<Role>>> Get(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
                page = 1;
            }

            int intPageSize = 50;

            var data = await _roleRepository.QueryPage(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");

            return new MessageModel<PageModel<Role>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };

        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] Role role)
        {
            var data = new MessageModel<string>();

            var id = (await _roleRepository.Add(role));
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] Role role)
        {
            var data = new MessageModel<string>();
            if (role != null && role.Id > 0)
            {
                data.success = await _roleRepository.Update(role);
                if (data.success)
                {
                    data.msg = "更新成功";
                    data.response = role?.Id.ObjToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var userDetail = await _roleRepository.QueryById(id);
                userDetail.IsDeleted = true;
                data.success = await _roleRepository.Update(userDetail);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = userDetail?.Id.ObjToString();
                }
            }

            return data;
        }
    }
}
