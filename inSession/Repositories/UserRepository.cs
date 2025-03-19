using inSession.Models;

namespace inSession.Repositories
{
    public class UserRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public async Task InsertUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
