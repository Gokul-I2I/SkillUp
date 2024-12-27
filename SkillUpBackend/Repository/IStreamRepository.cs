using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public interface IStreamRepository
    {
        Task AddStream(StreamModel streamModel);
        Task UpdateStream(StreamModel streamModel);
        Task<List<StreamModel>> GetAllStreams();
        Task<StreamModel> GetStreamById(int id);
    }
}
