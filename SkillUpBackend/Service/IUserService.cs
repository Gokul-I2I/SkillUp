using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Service
{
    public interface IUserService
    {
        Task ActiveUser(int id);
        Task AddUser(UserCreateOrEdit userCreateOrEdit);
        Task DeleteUser(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<UserCreateOrEdit> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task UpdateUser(int id, UserCreateOrEdit userCreateOrEdit);
        Task<User> ValidateUserCredentials(string email, string password);
    }
}
