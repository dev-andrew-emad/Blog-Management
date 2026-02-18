using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using blogdatalayer.data;
using blogdatalayer.entities;
using blogbusinesslayer.dtos;

namespace blogbusinesslayer.business
{
    public class userbusiness
    {
        private readonly userdata _userdata;
        private readonly jwtsetting _jwtsettings;
        public userbusiness(userdata userdata, IOptions<jwtsetting> jwtsetting)
        {
            _userdata = userdata;
            _jwtsettings = jwtsetting.Value;
        }
        public async Task<userdto> addnewuser(newuserdto newuserdto)
        {
            var user = new user
            {
                username = newuserdto.UserName,
                password = newuserdto.Password,
                role = "author"
            };
            int userid = await _userdata.addnewuser(user);
            if (userid != 0)
            {
                userdto userdto = new userdto();
                userdto.id = userid;
                userdto.username = user.username;
                userdto.role = user.role;
                return userdto;
            }
            else
            {
                return null;
            }
        }
        public async Task<string> login(logindto logindto)
        {
            var user = await _userdata.login(logindto.username, logindto.password);
            if (user == null)
                throw new Exception("wrong username or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.id.ToString()),
                new Claim(ClaimTypes.Role,user.role.Trim().ToLower())

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtsettings.issuer,
                audience: _jwtsettings.audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtsettings.durationhours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> updateuser(int userid, updateuserdto updateuserdto)
        {
            bool exist = await _userdata.isexist(userid);
            if (!exist)
                throw new Exception("user is not found");
            user user = new user()
            {
                id = userid,
                username = updateuserdto.username,

            };
            return await _userdata.updateuser(user);
        }
        public async Task<bool> updateuserbyadmin(int userid, updateuserbyadmindto updateuserbyadmindto)
        {
            bool exist = await _userdata.isexist(userid);
            if (!exist)
                throw new Exception("user is not found");
            user user = new user()
            {
                id = userid,
                username = updateuserbyadmindto.username,
                role = updateuserbyadmindto.role
            };
            return await _userdata.updateuserbyadmin(user);
        }
        public async Task<bool> changepassword(int id, changepassworddto changepassworddto)
        {
            var user = await _userdata.getuserbyid(id);
            if (user == null)
                throw new Exception("user is not found");

            bool varify = _userdata.varifypassword(user, user.password, changepassworddto.oldpassword);
            if (!varify)
                throw new Exception("old password is wrong");
            return await _userdata.changepassword(id, changepassworddto.newpassword);
        }
        public async Task<List<userdto>> getallusers()
        {
            var userslist = await _userdata.getallusers();
            var usersdtolist = new List<userdto>();
            foreach (var user in userslist)
            {
                userdto userdto = new userdto();
                userdto.id = user.id;
                userdto.username = user.username;
                userdto.role = user.role;
                usersdtolist.Add(userdto);

            }
            return usersdtolist;
        }
        public async Task<bool> deleteuser(int id)
        {
            bool exist = await _userdata.isexist(id);
            if (!exist)
                throw new Exception("user is not found");
            return await _userdata.deleteuser(id);
        }
    }
}
