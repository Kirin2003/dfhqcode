﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Achieve.AuthHelper;
using Student.Achieve.Common.Helper;
using Student.Achieve.Common.HttpContextUser;
using Student.Achieve.IRepository;
using Student.Achieve.Model;
using Student.Achieve.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Student.Achieve.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class PermissionController : ControllerBase
    {
        readonly IPermissionRepository _permissionRepository;
        readonly IModuleRepository _moduleRepository;
        readonly IRoleModulePermissionRepository _roleModulePermissionRepository;
        readonly IUserRoleRepository _userRoleRepository;
        readonly IHttpContextAccessor _httpContext;
        private readonly IUser _iUser;
        private readonly IRoleRepository _iRoleRepository;

       
        public PermissionController(IPermissionRepository permissionRepository, IModuleRepository moduleRepository, IRoleModulePermissionRepository roleModulePermissionRepository, IUserRoleRepository userRoleRepository, IHttpContextAccessor httpContext, IUser iUser,IRoleRepository iRoleRepository)
        {
            _permissionRepository = permissionRepository;
            _moduleRepository = moduleRepository;
            _roleModulePermissionRepository = roleModulePermissionRepository;
            _userRoleRepository = userRoleRepository;
            _httpContext = httpContext;
            this._iUser = iUser;
            this._iRoleRepository = iRoleRepository;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<MessageModel<PageModel<Permission>>> Get(int page = 1, string key = "")
        {
            PageModel<Permission> permissions = new PageModel<Permission>();
            int intPageSize = 50;
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
                page = 1;
            }

            #region 舍弃
            //var permissions = await _permissionRepository.Query(a => a.IsDeleted != true);
            //if (!string.IsNullOrEmpty(key))
            //{
            //    permissions = permissions.Where(t => (t.Name != null && t.Name.Contains(key))).ToList();
            //}
            ////筛选后的数据总数
            //totalCount = permissions.Count;
            ////筛选后的总页数
            //pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intTotalCount.ObjToDecimal())).ObjToInt();
            //permissions = permissions.OrderByDescending(d => d.Id).Skip((page - 1) * intTotalCount).Take(intTotalCount).ToList(); 
            #endregion



            permissions = await _permissionRepository.QueryPage(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");


            #region 单独处理

            var apis = await _moduleRepository.Query(d => d.IsDeleted == false);
            var permissionsView = permissions.data;

            var permissionAll = await _permissionRepository.Query(d => d.IsDeleted != true);
            foreach (var item in permissionsView)
            {
                List<int> pidarr = new List<int>
                {
                    item.Pid
                };
                if (item.Pid > 0)
                {
                    pidarr.Add(0);
                }
                var parent = permissionAll.FirstOrDefault(d => d.Id == item.Pid);

                while (parent != null)
                {
                    pidarr.Add(parent.Id);
                    parent = permissionAll.FirstOrDefault(d => d.Id == parent.Pid);
                }


                item.PidArr = pidarr.OrderBy(d => d).Distinct().ToList();
                foreach (var pid in item.PidArr)
                {
                    var per = permissionAll.FirstOrDefault(d => d.Id == pid);
                    item.PnameArr.Add((per != null ? per.Name : "根节点") + "/");
                    //var par = Permissions.Where(d => d.Pid == item.Id ).ToList();
                    //item.PCodeArr.Add((per != null ? $"/{per.Code}/{item.Code}" : ""));
                    //if (par.Count == 0 && item.Pid == 0)
                    //{
                    //    item.PCodeArr.Add($"/{item.Code}");
                    //}
                }

                item.MName = apis.FirstOrDefault(d => d.Id == item.Mid)?.LinkUrl;
            }

            permissions.data = permissionsView;

            #endregion


            return new MessageModel<PageModel<Permission>>()
            {
                msg = "获取成功",
                success = permissions.dataCount >= 0,
                response = permissions
            };

        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
        }

        /// <summary>
        /// 添加一个菜单
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] Permission permission)
        {
            var data = new MessageModel<string>();

            var id = (await _permissionRepository.Add(permission));
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 保存菜单权限分配
        /// </summary>
        /// <param name="assignView"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Assign([FromBody] AssignView assignView)
        {
            var data = new MessageModel<string>();

            try
            {
                if (assignView.rid > 0)
                {

                    data.success = true;

                    var roleModulePermissions = await _roleModulePermissionRepository.Query(d => d.RoleId == assignView.rid);

                    var remove = roleModulePermissions.Where(d => !assignView.pids.Contains(d.PermissionId.ObjToInt())).Select(c => (object)c.Id);
                    data.success |= await _roleModulePermissionRepository.DeleteByIds(remove.ToArray());

                    foreach (var item in assignView.pids)
                    {
                        var rmpitem = roleModulePermissions.Where(d => d.PermissionId == item);
                        if (!rmpitem.Any())
                        {
                            var moduleid = (await _permissionRepository.Query(p => p.Id == item)).FirstOrDefault()?.Mid;
                            RoleModulePermission roleModulePermission = new RoleModulePermission()
                            {
                                IsDeleted = false,
                                RoleId = assignView.rid,
                                ModuleId = moduleid.ObjToInt(),
                                PermissionId = item,
                            };

                            data.success |= (await _roleModulePermissionRepository.Add(roleModulePermission)) > 0;

                        }
                    }

                    if (data.success)
                    {
                        data.response = "";
                        data.msg = "保存成功";
                    }

                }
            }
            catch (Exception)
            {
                data.success = false;
            }

            return data;
        }


        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="needbtn"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PermissionTree>> GetPermissionTree(int pid = 0, bool needbtn = false)
        {
            var data = new MessageModel<PermissionTree>();

            var permissions = await _permissionRepository.Query(d => d.IsDeleted == false);
            var permissionTrees = (from child in permissions
                                   where child.IsDeleted == false
                                   orderby child.Id
                                   select new PermissionTree
                                   {
                                       value = child.Id,
                                       label = child.Name,
                                       Pid = child.Pid,
                                       isbtn = child.IsButton,
                                       order = child.OrderSort,
                                   }).ToList();
            PermissionTree rootRoot = new PermissionTree
            {
                value = 0,
                Pid = 0,
                label = "根节点"
            };

            permissionTrees = permissionTrees.OrderBy(d => d.order).ToList();


            RecursionHelper.LoopToAppendChildren(permissionTrees, rootRoot, pid, needbtn);

            data.success = true;
            if (data.success)
            {
                data.response = rootRoot;
                data.msg = "获取成功";
            }

            return data;
        }

        /// <summary>
        /// 获取路由树
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<NavigationBar>> GetNavigationBar(int uid)
        {

            var data = new MessageModel<NavigationBar>();

            // 两种方式获取 uid
            var uidInHttpcontext1 = (from item in _httpContext.HttpContext.User.Claims
                                    where item.Type == "jti"
                                    select item.Value).FirstOrDefault().ObjToInt();

            var uidInHttpcontext = (JwtToken.SerializeJwt(_httpContext.HttpContext.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "")))?.Uid;

            if (uid > 0 && uid == uidInHttpcontext)
            {
                var roleId = ((await _userRoleRepository.Query(d => d.IsDeleted == false && d.UserId == uid)).FirstOrDefault()?.RoleId).ObjToInt();
                var roletype = _iUser.GetClaimValueByType(ClaimTypes.Role);
                if (roletype.Contains("Teacher_Role"))
                {
                    roleId = ((await _iRoleRepository.Query(d => d.IsDeleted == false && d.Name == "Teacher_Role")).FirstOrDefault()?.Id).ObjToInt();
                }

                if (roleId > 0)
                {
                    var pids = (await _roleModulePermissionRepository.Query(d => d.IsDeleted == false && d.RoleId == roleId)).Select(d => d.PermissionId.ObjToInt()).Distinct();

                    if (pids.Any())
                    {
                        var rolePermissionMoudles = (await _permissionRepository.Query(d => pids.Contains(d.Id) && d.IsButton == false)).OrderBy(c => c.OrderSort);
                        var permissionTrees = (from child in rolePermissionMoudles
                                               where child.IsDeleted == false
                                               orderby child.Id
                                               select new NavigationBar
                                               {
                                                   id = child.Id,
                                                   name = child.Name,
                                                   pid = child.Pid,
                                                   order = child.OrderSort,
                                                   path = child.Code,
                                                   iconCls = child.Icon,
                                                   IsHide = child.IsHide.ObjToBool(),
                                                   meta = new NavigationBarMeta
                                                   {
                                                       requireAuth = child.IsAuth.ObjToBool(),
                                                       title = child.Name,
                                                       NoTabPage = child.IsHide.ObjToBool()
                                                   }
                                               }).ToList();


                        NavigationBar rootRoot = new NavigationBar()
                        {
                            id = 0,
                            pid = 0,
                            order = 0,
                            name = "根节点",
                            path = "",
                            iconCls = "",
                            meta = new NavigationBarMeta(),

                        };

                        permissionTrees = permissionTrees.OrderBy(d => d.order).ToList();

                        RecursionHelper.LoopNaviBarAppendChildren(permissionTrees, rootRoot);

                        data.success = true;
                        if (data.success)
                        {
                            data.response = rootRoot;
                            data.msg = "获取成功";
                        }
                    }
                    else
                    {
                        data.msg = "没有找到任何权限";
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 通过角色获取菜单【无权限】
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<AssignShow>> GetPermissionIdByRoleId(int rid = 0)
        {
            var data = new MessageModel<AssignShow>();

            var rmps = await _roleModulePermissionRepository.Query(d => d.IsDeleted == false && d.RoleId == rid);
            var permissionTrees = (from child in rmps
                                   orderby child.Id
                                   select child.PermissionId.ObjToInt()).ToList();

            var permissions = await _permissionRepository.Query(d => d.IsDeleted == false);
            List<string> assignbtns = new List<string>();

            foreach (var item in permissionTrees)
            {
                var pername = permissions.FirstOrDefault(d => d.IsButton && d.Id == item)?.Name;
                if (!string.IsNullOrEmpty(pername))
                {
                    //assignbtns.Add(pername + "_" + item);
                    assignbtns.Add(item.ObjToString());
                }
            }

            data.success = true;
            if (data.success)
            {
                data.response = new AssignShow()
                {
                    permissionids = permissionTrees,
                    assignbtns = assignbtns,
                };
                data.msg = "获取成功";
            }

            return data;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] Permission permission)
        {
            var data = new MessageModel<string>();
            if (permission != null && permission.Id > 0)
            {
                data.success = await _permissionRepository.Update(permission);
                if (data.success)
                {
                    data.msg = "更新成功";
                    data.response = permission?.Id.ObjToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 删除菜单
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
                var userDetail = await _permissionRepository.QueryById(id);
                userDetail.IsDeleted = true;
                data.success = await _permissionRepository.Update(userDetail);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = userDetail?.Id.ObjToString();
                }
            }

            return data;
        }
    }

    public class AssignView
    {
        public List<int> pids { get; set; }
        public int rid { get; set; }
    }
    public class AssignShow
    {
        public List<int> permissionids { get; set; }
        public List<string> assignbtns { get; set; }
    }

}