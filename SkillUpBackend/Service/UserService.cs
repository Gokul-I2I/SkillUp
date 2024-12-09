using SkillUpBackend.CustomException;
using SkillUpBackend.Model;
using SkillUpBackend.Repository;
using SkillUpBackend.Utils;

namespace SkillUpBackend.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly PasswordHelper _passwordHelper;
        

        public UserService(IUserRepository userRepository, IRoleService roleService, PasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _roleService = roleService;
            _passwordHelper = passwordHelper;
        }

        public async Task AddUser(UserCreateModel userCreateModel)
        {
            var user = MapToDomainModel(userCreateModel);
            await _userRepository.AddUser(user);
        }
        private User MapToDomainModel(UserCreateModel viewModel)
        {
            var user = new User
            {

                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email.ToLower(),
                Password = _passwordHelper.HashPassword(viewModel.Password),
                Role = _roleService.GetRoleById(int.Parse(viewModel.Role)).Result,
                InsertedBy = "Admin",
                InsertedOn = DateTime.Now
            };

            return user;
        }

        public async Task<User> ValidateUserCredentials(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email.ToLower());

            if (!_passwordHelper.IsValidPassword(password, user.Password))
            {
                throw new UnauthorizedAccessException("Password Mismatch");
            }
            return user;
        }


        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            user.IsActive = false;
            await _userRepository.UpdateUser(user);
        }

        public async Task<UserEditModel> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return new UserEditModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Qualification = user.Qualification,
            };
        }

        public async Task UpdateUser(int id, UserEditModel userEditModel)
        {
            var user = await _userRepository.GetUserById(id);
            user.FirstName = userEditModel.FirstName;
            user.LastName = userEditModel.LastName;
            user.DateOfBirth = userEditModel.DateOfBirth;
            user.Qualification = userEditModel.Qualification;
            user.UpdatedBy = user.FirstName;
            user.LastUpdatedOn = DateTime.Now;
            user.Password = _passwordHelper.HashPassword(userEditModel.Password);
            await _userRepository.UpdateUser(user);
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
    }
}
