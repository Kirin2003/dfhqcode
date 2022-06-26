using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Services;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendController : ControllerBase
    {
        // 根据id查推荐给用户的论文
        [HttpGet("userId")]
        public IActionResult GetCitations(string userId)
        {
            RecommendService r = new RecommendService();
            var result = r.recommend(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }
    }
}
