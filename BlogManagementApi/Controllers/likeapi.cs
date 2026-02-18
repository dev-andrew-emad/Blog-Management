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
    public class likeapi : ControllerBase
    {
        private readonly likebusiness _likebusiness;
        public likeapi(likebusiness likebusiness)
        {
            _likebusiness = likebusiness;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult>like(int postid)
        {
            if(postid<=0)
            {
                return BadRequest();
            }
            int authorid=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result = await _likebusiness.like(authorid, postid);
            if(result)
            {
                return Ok("post liked");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult>unlike(int postid)
        {
            if (postid <= 0)
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result = await _likebusiness.unlike(authorid, postid);
            if (result)
            {
                return Ok("post unliked");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet("likedposts")]
        public async Task<ActionResult<IEnumerable<string>>>getallpostsliked()
        {
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result=await _likebusiness.getallpostsliked(authorid);

            return Ok(result);
        }
    }
}
