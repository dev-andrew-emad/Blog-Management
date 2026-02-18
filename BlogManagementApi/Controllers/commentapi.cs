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
    public class commentapi : ControllerBase
    {
        private readonly commentbusiness _commentbusiness;
        public commentapi(commentbusiness commentbusiness)
        {
            _commentbusiness = commentbusiness;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<commentdto>>>getallcomments()
        {
            int authorid=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result=await _commentbusiness.getallcomments(authorid);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<commentdto>>addnewcomment(newcommentdto newcommentdto)
        {
            if(newcommentdto.postid<=0||string.IsNullOrEmpty(newcommentdto.content))
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _commentbusiness.addnewcomment(authorid, newcommentdto);
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
        public async Task<ActionResult<string>>updatecomment(int commentid,string content)
        {
            if(commentid<=0||string.IsNullOrEmpty(content))
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result=await _commentbusiness.updatecomment(authorid,commentid,content);

            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult>deletecomment(int commentid)
        {
            if(commentid<=0)
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result=await _commentbusiness.deletecomment(authorid, commentid);
            if(result)
            {
                return Ok("comment deleted successfully");
            }
            else
            {
                return BadRequest();
            }

        }
        [Authorize(Roles ="super admin")]
        [HttpDelete("deletecommentbyadmin")]
        public async Task<ActionResult> deletecommentbyadmin(int commentid)
        {
            if (commentid <= 0)
            {
                return BadRequest();
            }
            bool result = await _commentbusiness.deletecommentbyadmin(commentid);
            if (result)
            {
                return Ok("comment deleted successfully");
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
