using SkillUpBackend.Model;

namespace SkillUpBackend.Service
{
    public interface IRoleService
    {
        Task AddRole(RoleCreateModel roleCreateModel);
        Task DeleteRole(int id);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
    }
}
