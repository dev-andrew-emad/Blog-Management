using System.Security.Claims;
using blogbusinesslayer.business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commentlikeapi : ControllerBase
    {
        private readonly commentlikebusiness _commentlikebusiness;
        public commentlikeapi(commentlikebusiness commentlikebusiness)
        {
            _commentlikebusiness = commentlikebusiness;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult>likecomment(int commentid)
        {
            if(commentid<=0)
            {
                return BadRequest();
            }
            int authorid=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result = await _commentlikebusiness.likecomment(authorid, commentid);
            if(result)
            {
                return Ok("comment liked");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult>unlikecomment(int commentid)
        {
            if(commentid<=0)
            {
                return BadRequest();
            }
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result=await _commentlikebusiness.unlikecomment(authorid, commentid);
            if(result)
            {
                return Ok("comment unliked");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>>getallcommentsliked()
        {
            int authorid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result=await _commentlikebusiness.getallcommentsliked(authorid);
            return Ok(result);
        }
    }
}
