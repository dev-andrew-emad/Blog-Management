using System.Security.Claims;
using blogbusinesslayer.business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class replylikeapi : ControllerBase
    {
        private readonly replylikebusiness _replylikebusiness;
        public replylikeapi(replylikebusiness replylikebusiness)
        {
            _replylikebusiness = replylikebusiness;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult>likereply(int replyid)
        {
            if(replyid<=0)
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result = await _replylikebusiness.likereply(authorid, replyid);
            if(result)
            {
                return Ok("reply liked");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult>unlikereply(int replyid)
        {
            if(replyid<=0)
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result=await _replylikebusiness.unlikereply(authorid, replyid);
            if(result)
            {
                return Ok("reply unliked");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>>getallrepliesliked()
        {
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _replylikebusiness.getallrepliesliked(authorid);
            return Ok(result);
        }
    }
}
