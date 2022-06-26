using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Services;
using BackendCode.Models;
using Microsoft.AspNetCore.Hosting;

namespace TestWebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PaperController : ControllerBase
    {
        private RawPaperService paperService;
        private DynamicInfoService dynService;
        public PaperController()
        {
            paperService = new RawPaperService();
        }

        // 首页，默认展示所有论文
        [HttpGet]
        [Route("")]
        public IActionResult GetPapers(int page = 0, int pageSize = 10)
        {
            var results = paperService.QueryAll(page, pageSize);

            return Ok(results);
        }

        // 根据id查找论文
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = paperService.QueryDocById(id);
            var paperInfo = dynService.QueryDocById(id);
            paperInfo.ReadNum++;
            dynService.UpdateDocument(paperInfo, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        // 根据会议和关键词搜索论文
        [HttpGet("search")]
        public IActionResult SearchByMeetingAndKeyword([FromQuery]string meeting, string keyword, int page = 0, int pageSize = 10)
        {
            var results = paperService.SearchByMeetingAndKeyword(meeting, keyword, page, pageSize);
            if (results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        [HttpGet("search_by_keywords")]
        public IActionResult SearchByKeywords([FromQuery]string keywords, int page = 0, int pageSize = 10)
        {

            var results = paperService.SearchByKeywords(keywords, page, pageSize);
            if (results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        [HttpGet("get_count")]
        public IActionResult GetTotalCount()
        {
            var results = paperService.GetTotalCount();
            return Ok(results);
        }



    }
}
