using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<User> GetUserByIdToActive(int id);

    }
}
