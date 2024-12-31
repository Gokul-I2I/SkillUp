using SkillUpBackend.Model;
using SkillUpBackend.Repository;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Service
{
    public class StreamService : IStreamService
    {
        private readonly IStreamRepository _streamRepository;
        public StreamService(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }
        public async Task AddStream(StreamCreateOrEdit streamCreateOrEdit)
        {
            await _streamRepository.AddStream(
                new StreamModel() {
                        Name = streamCreateOrEdit.Name,
                        Description = streamCreateOrEdit.Description,
                        InsertedBy = "admin",
                        InsertedOn = DateTime.Now,
                 });
        }

        public async Task DeleteStream(int id)
        {
            var stream = await _streamRepository.GetStreamById(id);
            stream.IsActive = false;
            stream.UpdatedBy = "admin";
            stream.UpdatedOn = DateTime.Now;
            await _streamRepository.UpdateStream(stream);
        }

        public async Task<List<StreamModel>> GetAllStreams()
        {
            return await _streamRepository.GetAllStreams();   
        }

        public async Task<StreamModel> GetStreamById(int id)
        {
            return await _streamRepository.GetStreamById(id);
        }

        public async Task UpdateStream(int id, StreamCreateOrEdit streamModel)
        {
            var stream = await _streamRepository.GetStreamById(id);
            stream.Name = streamModel.Name;
            stream.Description = streamModel.Description;
            stream.UpdatedBy = "admin";
            stream.UpdatedOn = DateTime.Now;
            await _streamRepository.UpdateStream(stream);   
        }
    }
}
