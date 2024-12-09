using SkillUpBackend.Model;

namespace SkillUpBackend.Service
{
    public interface IUserService
    {
        Task AddUser(UserCreateModel userCreateModel);
        Task DeleteUser(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<UserEditModel> GetUserById(int id);
        Task UpdateUser(int id,UserEditModel userEditModel);
        Task<User> ValidateUserCredentials(string email, string password);
    }
}
