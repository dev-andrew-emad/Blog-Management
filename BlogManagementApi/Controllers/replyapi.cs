using System.ComponentModel;
using System.Security.Claims;
using blogbusinesslayer.business;
using blogbusinesslayer.dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class replyapi : ControllerBase
    {
        private readonly replybusiness _replybusiness;
        public replyapi(replybusiness replybusiness)
        {
            _replybusiness = replybusiness;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<replydto>>addnewreply(newreplydto newreplydto)
        {
            if(newreplydto.commentid<=0||string.IsNullOrEmpty(newreplydto.content))
            {
                return BadRequest();
            }
            int authorid=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result=await _replybusiness.addnewreply(authorid, newreplydto);
            if(result!=null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<string>>updatereply(int replyid,string content)
        {
            if(replyid<=0||string.IsNullOrEmpty(content))
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _replybusiness.updatereply(authorid, replyid, content);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult>deletereply(int replyid)
        {
            if(replyid<=0)
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result=await _replybusiness.deletereply(authorid,replyid);
            if(result)
            {
                return Ok("reply deleted successfully");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<replydto>>>getallreplies()
        {
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result=await _replybusiness.getallreplies(authorid);
            return Ok(result);
        }
        [Authorize(Roles ="super admin")]
        [HttpDelete("deletereplybyadmin")]
        public async Task<ActionResult> deletereplybyadmin(int replyid)
        {
            if (replyid <= 0)
            {
                return BadRequest();
            }
            bool result = await _replybusiness.deletereplybyadmin(replyid);
            if (result)
            {
                return Ok("reply deleted successfully");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
