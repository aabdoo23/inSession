using inSession.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inSession.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            User user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user.Id);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User user = await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Posts) //eager loading
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
