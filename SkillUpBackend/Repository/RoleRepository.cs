using Microsoft.EntityFrameworkCore;
using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRole(Role role)
        {
            try
            {
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public async Task UpdateRole(Role role)
        {
            try
            {
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return _context.Roles.Where(r => r.IsActive == true).ToList();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id && r.IsActive == true);
        }
    }
}
