using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blogdatalayer.dbcontext;
using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;

namespace blogdatalayer.data
{
    public class commentlikedata
    {
        private readonly appdbcontext _context;
        public commentlikedata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<bool>likecomment(commentlike commentlike)
        {
            _context.commentlikes.Add(commentlike);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool>unlikecomment(int authorid,int commentid)
        {
            var commentlike = await _context.commentlikes.FirstOrDefaultAsync(c => c.userid == authorid && c.commentid == commentid);
            _context.commentlikes.Remove(commentlike);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<commentlike>getcommentlikebyauthorandcommentid(int authorid,int commentid)
        {
            return await _context.commentlikes.FirstOrDefaultAsync(c => c.userid == authorid && c.commentid == commentid);
        }
        public async Task<List<commentlike>>getalllikes(int authorid)
        {
            return await _context.commentlikes.Include(c => c.comment).Where(c => c.userid == authorid).ToListAsync();
        }
    }
}
