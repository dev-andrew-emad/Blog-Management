using blogdatalayer.dbcontext;
using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;

namespace blogdatalayer.data
{
    public class postdata
    {
        private readonly appdbcontext _context;
        public postdata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<int> addnewpost(post newpost)
        {
            _context.posts.Add(newpost);
            await _context.SaveChangesAsync();
            return newpost.id;
        }
        public async Task<List<post>> getallposts(int authorid)
        {
            var list = await _context.posts.Include(p => p.author).Include(p => p.likes).Include(p => p.comments).ThenInclude(c => c.user).Include(p=>p.comments).ThenInclude(c=>c.commentlikes)
                .Include(p=>p.comments).ThenInclude(c=>c.replies).ThenInclude(r=>r.replylikes).Where(p => p.authorid == authorid).ToListAsync();
            return list;
        }
        public async Task<post> updatepost(post updatedpost)
        {
            var post = await _context.posts.FirstOrDefaultAsync(p => p.id == updatedpost.id);
            post.title = updatedpost.title;
            post.content = updatedpost.content;
            post.ispublished = updatedpost.ispublished;
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<post> getpostbyid(int id)
        {
            return await _context.posts.FirstOrDefaultAsync(p => p.id == id);
        }
        public async Task<bool> deletepost(int postid)
        {
            var post = await _context.posts.FirstOrDefaultAsync(p => p.id == postid);
            var likes = await _context.likes.Where(l => l.postid == postid).ToListAsync();
            var comments = await _context.comments.Where(c => c.postid == postid).ToListAsync();
            var commentids=comments.Select(c=>c.id).ToList();
            var commentlikes=await _context.commentlikes.Where(c=>commentids.Contains(c.commentid)).ToListAsync();
            var replies=await _context.replies.Where(r=>commentids.Contains(r.commentid)).ToListAsync();
            var replyids=replies.Select(r=>r.id).ToList();
            var replylikes = await _context.replylikes.Where(r => replyids.Contains(r.replyid)).ToListAsync();
            using var transaction = await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    _context.replylikes.RemoveRange(replylikes);
                    _context.replies.RemoveRange(replies);
                    _context.commentlikes.RemoveRange(commentlikes);
                    _context.comments.RemoveRange(comments);
                    _context.likes.RemoveRange(likes);
                    _context.posts.Remove(post);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<List<post>> getallpublishedposts()
        {
            return await _context.posts.Include(p => p.author).Include(p => p.comments).ThenInclude(c => c.user).Include(p=>p.comments).ThenInclude(c=>c.commentlikes).Include(p => p.likes)
               .Include(p=>p.comments).ThenInclude(c=>c.replies).ThenInclude(r=>r.replylikes).Where(p => p.ispublished == true).ToListAsync();
        }
    }
}
