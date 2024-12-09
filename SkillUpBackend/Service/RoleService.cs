using Microsoft.CodeAnalysis.CSharp.Syntax;
using SkillUpBackend.Model;
using SkillUpBackend.Repository;

namespace SkillUpBackend.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task AddRole(RoleCreateModel roleCreateModel)
        {
            var role = MapToDomainModel(roleCreateModel);
            await _roleRepository.AddRole(role);
            
        }
        private Role MapToDomainModel(RoleCreateModel roleCreate)
        {
            var role = new Role
            {
                Name = roleCreate.Name.ToLower(),
                InsertedBy = "Admin",
                InsertedOn = DateTime.Now
            };

            return role;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return  await _roleRepository.GetAllRoles();
        }

        public Task<Role> GetRoleById(int id)
        {
            return _roleRepository.GetRoleById(id);
        }
        public async Task DeleteRole(int id)
        {
            var role = await _roleRepository.GetRoleById(id);
            if (role == null)
            {
                throw new Exception();
            }  
            role.IsActive = false;
            _roleRepository.UpdateRole(role);
        }
    }
}
