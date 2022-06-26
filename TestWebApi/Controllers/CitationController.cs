using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Models;
using BackendCode.Services;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitationController : ControllerBase
    {
        private CitationService citationService;
        public CitationController()
        {
            citationService = new CitationService();
        }

        // 根据id查引用论文
        [HttpGet("{id}/citations")]
        public IActionResult GetCitations(string id,int page = 0, int pageSize = 10)
        {
            var result = citationService.GetCitations(id,page,pageSize);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        // 根据id查找被引论文
        [HttpGet("{id}/references")]
        public IActionResult GetReferences(string id, int page = 0, int pageSize = 10)
        {
            var result = citationService.GetReferences(id, page, pageSize);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }
    }
}
