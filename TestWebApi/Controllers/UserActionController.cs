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
  
    [ApiController]
    public class UserActionController : ControllerBase
    {
        private UserLikeService userLikeService;
        private UserCollectionService userCollectionService;
        private DynamicInfoService dynService;

        public UserActionController(UserLikeService userLikeService, UserCollectionService userCollectionService)
        {
            this.userLikeService = userLikeService;
            this.userCollectionService = userCollectionService;
        }

        // 新增收藏
        // POST: api/collection?userid= && paperid=
        [HttpPost("api/collection")]
        public IActionResult Collect([FromQuery]long userid, string paperid)
        {
            UserCollection userCollection = new UserCollection();
            userCollection.UserId = userid;
            userCollection.PaperId = paperid;
            userCollection.time = DateTime.Now;

            var result = userCollectionService.StoreUserCollection(userCollection);
            var paperInfo = dynService.QueryDocById(paperid);
            paperInfo.CollectionNum++;
            var result1 = dynService.UpdateDocument(paperInfo, paperid);
            if (result && result1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("已经添加过收藏");
            }
        }

        // 取消收藏
        // DELETE: api/collection?userid= && paperid=
        [HttpDelete("api/collection")]
        public IActionResult CancelCollect([FromQuery]long userid, string paperid)
        {
            var result = userCollectionService.DeleteUserCollection(userid, paperid);
            var paperInfo = dynService.QueryDocById(paperid);
            paperInfo.CollectionNum--;
            var result1 = dynService.UpdateDocument(paperInfo, paperid);
            if (result && result1 )
            {
                return Ok();
            }
            else
            {
                return BadRequest("已经取消过收藏");
            }
        }

        // 新增点赞
        // POST: api/like?userid= && paperid=
        [HttpPost("api/like")]
        public IActionResult Like([FromQuery] long userid, string paperid)
        {
            UserLike userLike = new UserLike();
            userLike.UserId = userid;
            userLike.PaperId = paperid;
            userLike.time = DateTime.Now;

            var result = userLikeService.StoreUserLike(userLike);
            var paperInfo = dynService.QueryDocById(paperid);
            paperInfo.LikeNum++;
            var result1 = dynService.UpdateDocument(paperInfo, paperid);

            if (result && result1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("已经添加过点赞");
            }
        }

        // 取消点赞
        // DELETE: api/like?userid= && paperid=
        [HttpDelete("api/like")]
        public IActionResult CancelLike([FromQuery] long userid, string paperid)
        {
            var result = userLikeService.DeleteUserLike(userid, paperid);
            var paperInfo = dynService.QueryDocById(paperid);
            paperInfo.LikeNum--;
            var result1 = dynService.UpdateDocument(paperInfo, paperid);
            if (result && result1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("已经取消过点赞");
            }
        }
    
    }
}
