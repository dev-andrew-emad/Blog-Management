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
    public class replydata
    {
        private readonly appdbcontext _context;
        public replydata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<int>addnewreply(reply reply)
        {
            _context.replies.Add(reply);
            await _context.SaveChangesAsync();
            return reply.id;
        }
        public async Task<bool>updatereply(int replyid,string content)
        {
            var reply=await _context.replies.FirstOrDefaultAsync(r=>r.id==replyid);
            reply.content= content;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<reply>getreplybyid(int replyid)
        {
            return await _context.replies.FirstOrDefaultAsync(r => r.id == replyid);
        }
        public async Task<bool>deletereply(int replyid)
        {
            var reply=await _context.replies.FirstOrDefaultAsync(r=>r.id == replyid);
            var replylikes = await _context.replylikes.Where(r => r.replyid == replyid).ToListAsync();
            using var transaction=await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    _context.replylikes.RemoveRange(replylikes);
                    _context.replies.Remove(reply);

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
        public async Task<List<reply>>getallreplies(int authorid)
        {
            return await _context.replies.Include(r => r.comment).ThenInclude(c=>c.post).Include(r=>r.replylikes).Where(r => r.userid == authorid)
                .ToListAsync();
        }
    }
}
