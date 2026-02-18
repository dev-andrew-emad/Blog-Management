using blogdatalayer.data;
using blogbusinesslayer.dtos;
using blogdatalayer.entities;

namespace blogbusinesslayer.business
{
    public class postbusiness
    {
        private readonly postdata _postdata;
        private readonly userdata _userdata;
        public postbusiness(postdata postdata, userdata userdata)
        {
            _postdata = postdata;
            _userdata = userdata;
        }
        public async Task<postdto>addnewpost(int authorid,newpostdto newpostdto)
        {
            var author=await _userdata.getuserbyid(authorid);
            var post = new post()
            {
                title = newpostdto.title,
                content = newpostdto.content,
                authorid = authorid,
                createdat = DateTime.Now,
                ispublished = newpostdto.ispublished
            };
            int postid=await _postdata.addnewpost(post);
            if(postid!=0)
            {
                var postdto=new postdto();
                postdto.id = postid;
                postdto.title = post.title;
                postdto.content = post.content;
                postdto.authorname = author.username;
                postdto.createdat = post.createdat;
                postdto.ispublished = post.ispublished;
                return postdto;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<postdto>>getallposts(int authorid)
        {
            var postlist=await _postdata.getallposts(authorid);
            var listdto=new List<postdto>();
            foreach(var post in postlist)
            {
               
                var postdto=new postdto();
                postdto.id = post.id;
                postdto.title = post.title;
                postdto.content = post.content;
                postdto.authorname=post.author.username;
                postdto.createdat=post.createdat;
                postdto.ispublished=post.ispublished;
                postdto.comments = post.comments.Select(c => new commentwithusername
                {
                    id = c.id,
                    authorname = c.user.username,
                    content = c.content,
                    replies = c.replies.Select(r => new replywithusername
                    {
                        id = r.id,
                        authorname = r.user.username,
                        content = r.content,
                        replylikes = r.replylikes.Count()
                    }).ToList(),
                    commentlikes = c.commentlikes.Count()

                }).ToList();
                postdto.postlikes = post.likes.Count();
                listdto.Add(postdto);
            }
            return listdto;
        }
        public async Task<newpostdto>updatepost(int postid,int authorid,newpostdto newpostdto)
        {
            var existingpost = await _postdata.getpostbyid(postid);
            if (existingpost == null)
                throw new Exception("post is not found");
            if (existingpost.authorid != authorid)
                throw new Exception("this post is not yours!");
            var updatedpost = new post()
            {
                id=postid,
                title = newpostdto.title,
                content = newpostdto.content,
                ispublished = newpostdto.ispublished
            };
            var post = await _postdata.updatepost(updatedpost);
            if(post!=null)
            {
                var postdto = new newpostdto();
                postdto.title=post.title;
                postdto.content=post.content;
                postdto.ispublished = post.ispublished;
                return postdto;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool>deletepost(int postid,int authorid)
        {
            var existingpost=await _postdata.getpostbyid(postid);
            if (existingpost == null)
                throw new Exception("post is not found");
            if (existingpost.authorid != authorid)
                throw new Exception("this post is not yours!");

            return await _postdata.deletepost(postid);
        }
        public async Task<List<postdto>>getallpublishedposts()
        {
            var postlist = await _postdata.getallpublishedposts();
            var listdto = new List<postdto>();
            foreach (var post in postlist)
            {
                var postdto = new postdto();
                postdto.id = post.id;
                postdto.title = post.title;
                postdto.content = post.content;
                postdto.authorname = post.author.username;
                postdto.createdat = post.createdat;
                postdto.ispublished = post.ispublished;
                postdto.comments = post.comments.Select(c => new commentwithusername
                {
                    id= c.id,
                    authorname = c.user.username,
                    content = c.content,
                    replies=c.replies.Select(r=>new replywithusername
                    {
                        id= r.id,
                        authorname= r.user.username,
                        content = r.content,
                        replylikes=r.replylikes.Count()
                    }).ToList(),
                    commentlikes=c.commentlikes.Count()
                }).ToList();
                postdto.postlikes = post.likes.Count();
                listdto.Add(postdto);
            }
            return listdto;
        }
        public async Task<bool>deletepostbyadmin(int postid)
        {
            var existingpost = await _postdata.getpostbyid(postid);
            if (existingpost == null)
                throw new Exception("post is not found");
            return await _postdata.deletepost(postid);

        }


    }
}
