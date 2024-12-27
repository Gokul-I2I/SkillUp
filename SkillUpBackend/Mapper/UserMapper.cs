using SkillUpBackend.Model;
using SkillUpBackend.Service;
using SkillUpBackend.Utils;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Mapper
{
    public class UserMapper
    {
        private readonly IRoleService _roleService;
        private readonly PasswordHelper _passwordHelper;
        public UserMapper(IRoleService roleService, PasswordHelper passwordHelper)
        {
            _roleService = roleService;
            _passwordHelper = passwordHelper;
        }

        public async Task<User> UserCreateOrEditToUser(UserCreateOrEdit userCreateOrEdit)
        {

            return new User
            {
                FirstName = userCreateOrEdit.FirstName,
                LastName = userCreateOrEdit.LastName,
                Email = userCreateOrEdit.Email.ToLower(),
                Password = _passwordHelper.HashPassword(userCreateOrEdit.Password),
                Role = _roleService.GetRoleById(int.Parse(userCreateOrEdit.Role)).Result,
                InsertedBy = "Admin",
                InsertedOn = DateTime.Now
            };
        }

        public async Task<UserCreateOrEdit> UserToUserCreateOrEdit(User user)
        {
            return new UserCreateOrEdit
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Qualification = user.Qualification,
                Email = user.Email,
                Role = user.Role.Name,
                
            };
        }
    }
}
