using blogdatalayer.dbcontext;
using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;

namespace blogdatalayer.data
{
    public class likedata
    {
        private readonly appdbcontext _context;
        public likedata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<bool> like(like like)
        {
            _context.likes.Add(like);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> unlike(int authorid, int postid)
        {
            var like = await _context.likes.FirstOrDefaultAsync(l => l.userid == authorid && l.postid == postid);

            _context.likes.Remove(like);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<like> getlikebyauthorandpostid(int authorid, int postid)
        {
            return await _context.likes.FirstOrDefaultAsync(l => l.userid == authorid && l.postid == postid);
        }
        public async Task<List<like>> getalllikes(int authorid)
        {
            return await _context.likes.Include(l => l.post).Where(l => l.userid == authorid).ToListAsync();
        }
    }
}
