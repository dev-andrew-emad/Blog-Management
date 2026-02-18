using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blogdatalayer.data;
using blogdatalayer.entities;

namespace blogbusinesslayer.business
{
    public class replylikebusiness
    {
        private readonly replylikedata _replylikedata;
        private readonly replydata _replydata;
        public replylikebusiness(replylikedata replylikedata, replydata replydata)
        {
            _replylikedata = replylikedata;
            _replydata = replydata;
        }
        public async Task<bool>likereply(int authorid,int replyid)
        {
            var reply = await _replydata.getreplybyid(replyid);
            if (reply == null)
                throw new Exception("reply is not found");
            var like=await _replylikedata.getreplylikebyauthorandreplyid(authorid,replyid);
            if (like != null)
                throw new Exception("you have liked this reply before");
            var newlike = new replylike
            {
                userid = authorid,
                replyid = replyid
            };
            return await _replylikedata.likereply(newlike);
        }
        public async Task<bool>unlikereply(int authorid,int replyid)
        {
            var reply=await _replydata.getreplybyid(replyid);
            if (reply == null)
                throw new Exception("reply is not found");
            var like=await _replylikedata.getreplylikebyauthorandreplyid(authorid, replyid);
            if (like == null)
                throw new Exception("you didn't like this reply");
            return await _replylikedata.unlikereply(authorid,replyid);
        }
        public async Task<List<string>>getallrepliesliked(int authorid)
        {
            var likeslist = await _replylikedata.getallreplylikes(authorid);
            var replieslist=new List<string>();
            foreach(var like in likeslist)
            {
                replieslist.Add(like.reply.content);
            }
            return replieslist;
        }
    }
}
