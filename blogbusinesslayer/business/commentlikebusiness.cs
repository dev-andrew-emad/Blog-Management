using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blogdatalayer.data;
using blogdatalayer.entities;

namespace blogbusinesslayer.business
{
    public class commentlikebusiness
    {
        private readonly commentlikedata _commentlikedata;
        private readonly commentdata _commentdata;
        public commentlikebusiness(commentlikedata commentlikedata, commentdata commentdata)
        {
            _commentlikedata = commentlikedata;
            _commentdata = commentdata;
        }
        public async Task<bool>likecomment(int authorid,int commentid)
        {
            var comment = await _commentdata.getcommentbyid(commentid);
            if (comment == null)
                throw new Exception("comment is not found");
            var like = await _commentlikedata.getcommentlikebyauthorandcommentid(authorid, commentid);
            if (like != null)
                throw new Exception("you have liked this comment before");
            var newlike = new commentlike
            {
                userid = authorid,
                commentid = commentid
            };
            return await _commentlikedata.likecomment(newlike);
        }
        public async Task<bool>unlikecomment(int authorid,int commentid)
        {
            var comment=await _commentdata.getcommentbyid(commentid);
            if (comment == null)
                throw new Exception("comment is not found");
            var like=await _commentlikedata.getcommentlikebyauthorandcommentid(authorid,commentid);
            if (like == null)
                throw new Exception("you didn't like this comment");
            return await _commentlikedata.unlikecomment(authorid, commentid);
        }
        public async Task<List<string>>getallcommentsliked(int authorid)
        {
            var likeslist = await _commentlikedata.getalllikes(authorid);
            var commentslist=new List<string>();
            foreach(var like in likeslist)
            {
                commentslist.Add(like.comment.content);
            }
            return commentslist;
        }
    }
}
