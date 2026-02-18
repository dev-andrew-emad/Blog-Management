using blogdatalayer.data;
using blogbusinesslayer.dtos;
using blogdatalayer.entities;

namespace blogbusinesslayer.business
{
    public class likebusiness
    {
        private readonly likedata _likedata;
        private readonly postdata _postdata;
        public likebusiness(likedata likedata,postdata postdata)
        {
            _likedata = likedata;
            _postdata = postdata;
        }
        public async Task<bool>like(int authorid,int postid)
        {
            var post = await _postdata.getpostbyid(postid);
            if (post == null)
                throw new Exception("post is not found");
            var like = await _likedata.getlikebyauthorandpostid(authorid, postid);
            if (like != null)
                throw new Exception("you have liked this post before");
            var newlike = new like
            {
                userid = authorid,
                postid = postid,
                createdat = DateTime.Now
            };

            return await _likedata.like(newlike);
        }
        public async Task<bool>unlike(int authorid,int postid)
        {
            var post=await _postdata.getpostbyid(postid);
            if (post == null)
                throw new Exception("post is not found");
            var like = await _likedata.getlikebyauthorandpostid(authorid, postid);
            if (like == null)
                throw new Exception("you didn't like this post");

            return await _likedata.unlike(authorid,postid);
        }
        public async Task<List<string>>getallpostsliked(int authorid)
        {
            var likeslist=await _likedata.getalllikes(authorid);
            var postslist=new List<string>();
            foreach(var like in  likeslist)
            {
                postslist.Add(like.post.content);
            }
            return postslist;
        }
    }
}
