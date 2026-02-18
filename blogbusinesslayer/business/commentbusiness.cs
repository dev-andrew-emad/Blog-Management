
using blogdatalayer.entities;
using blogdatalayer.data;
using blogbusinesslayer.dtos;

namespace blogbusinesslayer.business
{
    public class commentbusiness
    {
        private readonly commentdata _commentdata;
        private readonly postdata _postdata;
        public commentbusiness(commentdata commentdata,postdata postdata)
        {
            _commentdata = commentdata;
            _postdata = postdata;
        }
        public async Task<List<commentdto>>getallcomments(int authorid)
        {
            var commentlist = await _commentdata.getallcomments(authorid);
            var commentlistdto=new List<commentdto>();
            foreach(var comment in commentlist)
            {
                commentdto commentdto = new commentdto();
                commentdto.id= comment.id;
                commentdto.postcontent = comment.post.content;
                commentdto.commentcontent = comment.content;
                commentdto.replies = comment.replies.Select(r => r.content).ToList();
                commentdto.commentlikes = comment.commentlikes.Count();
                commentdto.createdat= comment.createdat;
                commentlistdto.Add(commentdto);
            }
            return commentlistdto;
        }
        public async Task<commentdto>addnewcomment(int authorid,newcommentdto newcommentdto)
        {
            var post = await _postdata.getpostbyid(newcommentdto.postid);
            if (post == null)
                throw new Exception("post is not found");

            var comment = new comment
            {
                content = newcommentdto.content,
                userid = authorid,
                postid = newcommentdto.postid,
                createdat = DateTime.Now
            };
            int commentid =await _commentdata.addnewcomment(comment);
            if(commentid != 0)
            {
                commentdto commentdto=new commentdto();
                commentdto.id= commentid;
                commentdto.postcontent = post.content;
                commentdto.commentcontent=comment.content;
                commentdto.createdat= comment.createdat;
                return commentdto;
            }
            else
            {
                return null;
            }
        }
        public async Task<string>updatecomment(int authorid,int commentid,string content)
        {
            var comment=await _commentdata.getcommentbyid(commentid);
            if (comment == null)
                throw new Exception("comment is not found");
            if (comment.userid != authorid)
                throw new Exception("this comment is not yours");
            bool result = await _commentdata.updatecomment(commentid, content);
            if(result)
            {
                return content;
            }
            else
            {
                return "";
            }
        }
        public async Task<bool>deletecomment(int authorid,int commentid)
        {
            var comment = await _commentdata.getcommentbyid(commentid);
            if (comment == null)
                throw new Exception("comment is not found");
            if (comment.userid != authorid)
                throw new Exception("this comment is not yours");

            return await _commentdata.deletecomment(commentid);
        }
        public async Task<bool>deletecommentbyadmin(int commentid)
        {
            var comment = await _commentdata.getcommentbyid(commentid);
            if (comment == null)
                throw new Exception("comment is not found");
            return await _commentdata.deletecomment(commentid);

        }
    }
}
