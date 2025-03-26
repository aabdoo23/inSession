using inSession.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inSession.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            Post post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                Tags = request.Tags,
                UserId = request.UserId,
                CreatedAt = DateTime.Now
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return Ok(CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post));
        }

        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(int id)
        {
            Post post = await _context.Posts
                .Where(p => p.Id == id)
                //.Include(p => p.Comments) //eager loading
                .FirstOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
    }
}
