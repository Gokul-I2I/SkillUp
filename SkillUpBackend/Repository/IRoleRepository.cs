using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public interface IRoleRepository
    {
        Task AddRole(Role role);
        Task UpdateRole(Role role);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
    }
}
