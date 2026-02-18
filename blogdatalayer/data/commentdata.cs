using blogdatalayer.dbcontext;
using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;

namespace blogdatalayer.data
{
    public class commentdata
    {
        private readonly appdbcontext _context;
        public commentdata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<List<comment>> getallcomments(int authorid)
        {
            return await _context.comments.Include(c => c.post).Include(c=>c.replies).Include(c=>c.commentlikes).Where(c => c.userid == authorid)
                .ToListAsync();
        }
        public async Task<int> addnewcomment(comment comment)
        {
            _context.comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment.id;
        }
        public async Task<comment> getcommentbyid(int id)
        {
            return await _context.comments.Include(c=>c.post).FirstOrDefaultAsync(c => c.id == id);
        }
        public async Task<bool> updatecomment(int id, string content)
        {
            var comment = await _context.comments.FirstOrDefaultAsync(c => c.id == id);
            comment.content = content;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> deletecomment(int commentid)
        {
            var comment = await _context.comments.FirstOrDefaultAsync(c => c.id == commentid);
            var likes = await _context.commentlikes.Where(c => c.commentid == commentid).ToListAsync();
            var replies=await _context.replies.Where(r=>r.commentid == commentid).ToListAsync();
            var replyids=replies.Select(r => r.id).ToList();
            var replylikes =await _context.replylikes.Where(r=>replyids.Contains(r.replyid)).ToListAsync();
            using var transaction=await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    _context.replylikes.RemoveRange(replylikes);
                    _context.replies.RemoveRange(replies);
                    _context.commentlikes.RemoveRange(likes);
                    _context.comments.Remove(comment);

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
    }
}
