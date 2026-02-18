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
    public class postapi : ControllerBase
    {
        private readonly postbusiness _postbusiness;
        public postapi(postbusiness postbusiness)
        {
            _postbusiness = postbusiness;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult>addnewpost(newpostdto newpostdto)
        {
            int authorid=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result=await _postbusiness.addnewpost(authorid, newpostdto);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<postdto>>>getallposts()
        {
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result=await _postbusiness.getallposts(authorid);
            return Ok(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<newpostdto>>updatepost(int postid,newpostdto newpostdto)
        {
            if(string.IsNullOrEmpty(newpostdto.title)||string.IsNullOrEmpty(newpostdto.content))
            {
                return BadRequest("some fields are required");
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _postbusiness.updatepost(postid, authorid, newpostdto);
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
        [HttpDelete]
        public async Task<ActionResult>deletepost(int postid)
        {
            if(postid<=0)
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result=await _postbusiness.deletepost(postid,authorid);
            if(!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("post deleted successfully");
            }
        }
        [Authorize]
        [HttpGet("publishedposts")]
        public async Task<ActionResult<IEnumerable<postdto>>>getallpublishedposts()
        {
            var result= await _postbusiness.getallpublishedposts();
            return Ok(result);
        }
        [Authorize(Roles ="super admin")]
        [HttpDelete("deletepostbyadmin")]
        public async Task<ActionResult> deletepostbyadmin(int postid)
        {
            if (postid <= 0)
            {
                return BadRequest();
            }
            var result = await _postbusiness.deletepostbyadmin(postid);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("post deleted successfully");
            }
        }

    }
}
