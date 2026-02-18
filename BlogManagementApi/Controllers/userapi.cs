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
    public class userapi : ControllerBase
    {
        private readonly userbusiness _userbusiness;
        public userapi(userbusiness userbusiness)
        {
            _userbusiness = userbusiness;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<userdto>>addnewuser(newuserdto newuserdto)
        {
            if(string.IsNullOrEmpty(newuserdto.UserName)||string.IsNullOrEmpty(newuserdto.Password))
            {
                return BadRequest();
            }
            var user=await _userbusiness.addnewuser(newuserdto);
            if(user==null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult>login(logindto logindto)
        {
            if(string.IsNullOrEmpty(logindto.username)||string.IsNullOrEmpty(logindto.password))
            {
                return BadRequest();
            }
            var result=await _userbusiness.login(logindto);
            if(result==null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult>updateuser(updateuserdto updateuserdto)
        {
            if(string.IsNullOrEmpty(updateuserdto.username))
            {
                return BadRequest();
            }
            int userid =int.Parse( User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            bool result = await _userbusiness.updateuser(userid, updateuserdto);
            if(!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("user updated successfully");
            }
        }
        [Authorize(Roles ="super admin")]
        [HttpPut("updatebyuser")]
        public async Task<ActionResult>updateuserbyadmin(int id,updateuserbyadmindto updateuserbyadmindto)
        {
            if(id<=0||string.IsNullOrEmpty(updateuserbyadmindto.username)||string.IsNullOrEmpty(updateuserbyadmindto.role))
            {
                return BadRequest("some fields are required");
            }
            bool result=await _userbusiness.updateuserbyadmin(id,updateuserbyadmindto);
            if(!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("user updated successfully");
            }
        }
        [Authorize]
        [HttpPut("changepassword")]
        public async Task<ActionResult>changepassword(changepassworddto changepassworddto)
        {
            if(string.IsNullOrEmpty(changepassworddto.oldpassword)||string.IsNullOrEmpty(changepassworddto.newpassword))
            {
                return BadRequest("some fields are required");
            }
            int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result = await _userbusiness.changepassword(userid, changepassworddto);
            if(!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("password changed successfully");
            }
        }
        [Authorize(Roles ="super admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<userdto>>>getallusers()
        {
            var result=await _userbusiness.getallusers();

            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult>deleteuser()
        {
            int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool result=await _userbusiness.deleteuser(userid);
            if(!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("user deleted successfully");
            }
        }
        [Authorize(Roles ="super admin")]
        [HttpDelete("deletebyuserid")]
        public async Task<ActionResult>deleteuserbyid(int id)
        {
            if(id<=0)
            {
                return BadRequest();
            }
            bool result = await _userbusiness.deleteuser(id);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("user deleted successfully");
            }
        }
    }
}
