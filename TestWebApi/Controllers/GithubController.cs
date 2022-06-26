using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Services;
using BackendCode.Models;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubController : ControllerBase
    {
        private GithubService githubService;
        public GithubController()
        {
            githubService = new GithubService();
        }

        // 根据id查找论文
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = githubService.QueryDocById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }


    }
}
