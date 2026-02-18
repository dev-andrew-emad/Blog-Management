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
    public class replylikedata
    {
        private readonly appdbcontext _context;
        public replylikedata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<bool>likereply(replylike replylike)
        {
            _context.replylikes.Add(replylike);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool>unlikereply(int authorid,int replyid)
        {
            var replylike = await _context.replylikes.FirstOrDefaultAsync(r => r.userid == authorid && r.replyid == replyid);
            _context.replylikes.Remove(replylike);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<replylike>getreplylikebyauthorandreplyid(int authorid,int replyid)
        {
            return await _context.replylikes.FirstOrDefaultAsync(r => r.userid == authorid && r.replyid == replyid);
        }
        public async Task<List<replylike>>getallreplylikes(int authorid)
        {
            return await _context.replylikes.Include(r => r.reply).Where(r => r.userid == authorid).ToListAsync();
        }
    }
}
