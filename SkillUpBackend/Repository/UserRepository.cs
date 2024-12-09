using Microsoft.EntityFrameworkCore;
using SkillUpBackend.CustomException;
using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email && u.IsActive == true);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id && u.IsActive == true);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return _context.Users.OrderDescending().Include(u => u.Role).ToList();
        }
    }
}
