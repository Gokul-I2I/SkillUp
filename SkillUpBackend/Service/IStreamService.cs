using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Service
{
    public interface IStreamService
    {
        Task AddStream(StreamCreateOrEdit streamCreateOrEdit);
        Task UpdateStream(int id, StreamCreateOrEdit streamModel);
        Task<List<StreamModel>> GetAllStreams();
        Task<StreamModel> GetStreamById(int id);
        Task DeleteStream(int id);
    }
}
