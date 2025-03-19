using inSession.Models;
using Microsoft.EntityFrameworkCore;

namespace inSession.Repositories
{
    public class PostRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts
                .Where(p => p.Id == id)
                .Include(p => p.Comments.OrderBy(c => c.UpVotes).Take(2))
                .FirstOrDefaultAsync() ?? new Post();
        }

        public async Task<List<Comment>> GetPostComments(int id)
        {
            return await _context.Comments
                .Where(c => c.PostId == id)
                .OrderBy(c => c.UpVotes)
                .Take(20)
                .ToListAsync();
        }
        public async Task InsertPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }
        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    }
}
