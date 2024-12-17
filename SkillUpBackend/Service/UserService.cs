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

        public async Task AddUser(UserCreateOrEdit userCreateOrEdit)
        {
            User userDetails = await _userRepository.GetUserByEmail(userCreateOrEdit.Email.ToLower());
            if(userDetails != null)
            {
                throw new UserAlreadyExitException($"user already present in this email : {userCreateOrEdit.Email}");
            }
            var user = MapToDomainModel(userCreateOrEdit);
            await _userRepository.AddUser(user);
        }
        private User MapToDomainModel(UserCreateOrEdit viewModel)
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
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            user.IsActive = false;
            await _userRepository.UpdateUser(user);
        }

        public async Task<UserCreateOrEdit> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (!user.IsActive)
            {
                throw new UserNotFoundException();
            }
            return new UserCreateOrEdit
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Qualification = user.Qualification,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role.Name,
            };
        }

        public async Task UpdateUser(int id, UserCreateOrEdit userCreateOrEdit)
        {
            var user = await _userRepository.GetUserById(id);
            if (!user.IsActive)
            {
                throw new UserNotFoundException();
            }
            user.FirstName = userCreateOrEdit.FirstName;
            user.LastName = userCreateOrEdit.LastName;
            user.DateOfBirth = userCreateOrEdit.DateOfBirth;
            user.Qualification = userCreateOrEdit.Qualification;
            user.UpdatedBy = user.FirstName;
            user.LastUpdatedOn = DateTime.Now;
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
