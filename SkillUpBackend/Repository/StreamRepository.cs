using Microsoft.EntityFrameworkCore;
using SkillUpBackend.CustomException;
using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public class StreamRepository : IStreamRepository
    {
        private readonly ApplicationDbContext _context;
        public StreamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddStream(StreamModel streamModel)
        {
            await _context.Streams.AddAsync(streamModel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StreamModel>> GetAllStreams()
        {
            return await _context.Streams.Where(s => s.IsActive).ToListAsync();
        }

        public async Task<StreamModel> GetStreamById(int id)
        {
            var stream = await _context.Streams.Include(s => s.BatchStreams).FirstOrDefaultAsync(s => s.IsActive);
            if (stream == null)
            {
                throw new StreamNotFoundException();
            }
            return stream;
        }

        public async Task UpdateStream(StreamModel streamModel)
        {
            _context.Streams.Update(streamModel);
            await _context.SaveChangesAsync();
        }
    }
}
