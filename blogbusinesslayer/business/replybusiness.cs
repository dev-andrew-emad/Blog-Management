using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blogbusinesslayer.dtos;
using blogdatalayer.data;
using blogdatalayer.entities;

namespace blogbusinesslayer.business
{
    public class replybusiness
    {
        private readonly replydata _replydata;
        private readonly commentdata _commentdata;
        public replybusiness(replydata replydata, commentdata commentdata)
        {
            _replydata = replydata;
            _commentdata = commentdata;
        }
        public async Task<replydto>addnewreply(int authorid,newreplydto newreply)
        {
            var comment = await _commentdata.getcommentbyid(newreply.commentid);
            if (comment == null)
                throw new Exception("comment is not found");
            var reply = new reply
            {
                userid = authorid,
                content = newreply.content,
                commentid = newreply.commentid
            };
            int replyid=await _replydata.addnewreply(reply);
            if(replyid!=0)
            {
                replydto replydto = new replydto
                {
                    id = replyid,
                    post=comment.post.content,
                    comment = comment.content,
                    reply = newreply.content
                };
                return replydto;
            }
            else
            {
                return null;
            }
        }
        public async Task<string>updatereply(int authorid,int replyid,string content)
        {
            var reply=await _replydata.getreplybyid(replyid);
            if (reply == null)
                throw new Exception("reply is not found");
            if (reply.userid != authorid)
                throw new Exception("this reply is not yours");
            bool result=await _replydata.updatereply(replyid,content);
            if(result)
            {
                return content;
            }
            else
            {
                return "";
            }
        }
        public async Task<bool>deletereply(int authorid,int replyid)
        {
            var reply = await _replydata.getreplybyid(replyid);
            if (reply == null)
                throw new Exception("reply is not found");
            if (reply.userid != authorid)
                throw new Exception("this reply is not yours");
            return await _replydata.deletereply(replyid);
        }
        public async Task<bool>deletereplybyadmin(int replyid)
        {
            var reply = await _replydata.getreplybyid(replyid);
            if (reply == null)
                throw new Exception("reply is not found");
            return await _replydata.deletereply(replyid);

        }
        public async Task<List<replydto>>getallreplies(int authorid)
        {
            var replieslist=await _replydata.getallreplies(authorid);
            var replydtolist=new List<replydto>();
            foreach(var reply in replieslist)
            {
                replydto replydto = new replydto();
                replydto.id = reply.id;
                replydto.post = reply.comment.post.content;
                replydto.comment = reply.comment.content;
                replydto.reply = reply.content;
                replydto.replylikes = reply.replylikes.Count();
                replydtolist.Add(replydto);
            }
            return replydtolist;
        }
    }
}
