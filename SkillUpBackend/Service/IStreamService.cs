using SkillUpBackend.Model;

namespace SkillUpBackend.Service
{
    public interface IStreamService
    {
        //Task AddStream(StreamCreateOrEdit streamCreateOrEdit);
        Task UpdateStream(StreamModel streamModel);
        Task<List<StreamModel>> GetAllStreams();
        Task<StreamModel> GetStreamById(int id);
    }
}
