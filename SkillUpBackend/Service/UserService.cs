using SkillUpBackend.CustomException;
using SkillUpBackend.Mapper;
using SkillUpBackend.Model;
using SkillUpBackend.Repository;
using SkillUpBackend.Utils;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHelper _passwordHelper;
        private readonly IRoleService _roleService;
        private readonly UserMapper _userMapper;

        public UserService(IUserRepository userRepository, PasswordHelper passwordHelper, UserMapper userMapper, IRoleService roleService)
        {
            _userMapper = userMapper;
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _roleService = roleService;
        }

        public async Task AddUser(UserCreateOrEdit userCreateOrEdit)
        {
            User userDetails = await _userRepository.GetUserByEmail(userCreateOrEdit.Email.ToLower());
            if(userDetails != null)
            {
                throw new UserAlreadyExitException($"user already present in this email : {userCreateOrEdit.Email}");
            }
            var user = await _userMapper.UserCreateOrEditToUser(userCreateOrEdit);
            await _userRepository.AddUser(user);
        }

        public async Task<User> ValidateUserCredentials(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email.ToLower());
            if(user == null)
            {
                throw new UserNotFoundException();
            }
            if (!_passwordHelper.IsValidPassword(password, user.Password))
            {
                throw new UnauthorizedAccessException("Password Mismatch");
            }
            return user;
        }


        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            user.IsActive = false;
            await _userRepository.UpdateUser(user);
        }

        public async Task<UserCreateOrEdit> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return await _userMapper.UserToUserCreateOrEdit(user);
        }

        public async Task UpdateUser(int id, UserCreateOrEdit userCreateOrEdit)
        {
            var user = await _userRepository.GetUserById(id);
            user.FirstName = userCreateOrEdit.FirstName;
            user.LastName = userCreateOrEdit.LastName;
            user.DateOfBirth = userCreateOrEdit.DateOfBirth;
            user.Qualification = userCreateOrEdit.Qualification;
            user.UpdatedBy = user.FirstName;
            user.UpdatedOn = DateTime.Now;
            user.Password = _passwordHelper.HashPassword(userCreateOrEdit.Password);
            user.Role = _roleService.GetRoleById(int.Parse(userCreateOrEdit.Role)).Result;
            await _userRepository.UpdateUser(user);
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task ActiveUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            user.IsActive = true;
            await _userRepository.UpdateUser(user);
        }
    }
}
