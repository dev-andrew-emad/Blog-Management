using blogdatalayer.dbcontext;
using blogdatalayer.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace blogdatalayer.data
{
    public class userdata
    {
        private readonly appdbcontext _context;
        private readonly PasswordHasher<user> _hasher = new PasswordHasher<user>();
        public userdata(appdbcontext context)
        {
            _context = context;
        }
        public string hashpassword(user user, string password)
        {
            return _hasher.HashPassword(user, password);
        }
        public bool varifypassword(user user, string hashedpassword, string providedpassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedpassword, providedpassword);
            return result == PasswordVerificationResult.Success;
        }
        public async Task<int> addnewuser(user newuser)
        {
            newuser.password = hashpassword(newuser, newuser.password);
            _context.users.Add(newuser);
            await _context.SaveChangesAsync();
            return newuser.id;
        }
        public async Task<user> login(string username, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.username == username);
            if (user != null)
            {
                bool varify = varifypassword(user, user.password, password);
                if (varify)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> isexist(int userid)
        {
            return await _context.users.AnyAsync(u => u.id == userid);
        }
        public async Task<bool> updateuser(user updateduser)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.id == updateduser.id);
            user.username = updateduser.username;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> updateuserbyadmin(user updateduser)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.id == updateduser.id);
            user.username = updateduser.username;
            user.role = updateduser.role;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> changepassword(int userid, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.id == userid);
            user.password = hashpassword(user, password);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<user> getuserbyid(int id)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.id == id);
        }
        public async Task<List<user>> getallusers()
        {
            return await _context.users.ToListAsync();
        }
        public async Task<bool> deleteuser(int id)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.id == id);
            var posts = await _context.posts.Where(p => p.authorid == id).ToListAsync();
            var comments = await _context.comments.Where(c => c.userid == id).ToListAsync();
            var commentlikes=await _context.commentlikes.Where(c=>c.userid == id).ToListAsync();
            var likes = await _context.likes.Where(l => l.userid == id).ToListAsync();
            var replies=await _context.replies.Where(r=>r.userid == id).ToListAsync();
            var replylikes=await _context.replylikes.Where(r=>r.userid==id).ToListAsync();
            using var transaction = await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    _context.replylikes.RemoveRange(replylikes);
                    _context.replies.RemoveRange(replies);
                    _context.likes.RemoveRange(likes);
                    _context.commentlikes.RemoveRange(commentlikes);
                    _context.comments.RemoveRange(comments);
                    _context.posts.RemoveRange(posts);

                    _context.users.Remove(user);

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
