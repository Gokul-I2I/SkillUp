using SkillUpBackend.Model;
using SkillUpBackend.Repository;

namespace SkillUpBackend.Service
{
    public class StreamService : IStreamService
    {
        private readonly IStreamRepository _streamRepository;
        public StreamService(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }
        public Task AddStream(StreamModel streamModel)
        {
        throw new Exception();
        }

        public Task<List<StreamModel>> GetAllStreams()
        {
            throw new NotImplementedException();
        }

        public Task<StreamModel> GetStreamById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStream(StreamModel streamModel)
        {
            throw new NotImplementedException();
        }
    }
}
