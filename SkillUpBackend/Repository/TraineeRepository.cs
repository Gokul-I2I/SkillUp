using Microsoft.EntityFrameworkCore;
using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public class TraineeRepository : ITraineeRepository
    {

        private ApplicationDbContext _context;
        public TraineeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicAsync()
        {
            return await _context.Topics.Include(t => t.Subtopics)
                         .OrderByDescending(t => t.CreatedOn) // Assuming Topic has a CreatedOn field
                         .ToListAsync();
        }

        public async Task<IEnumerable<UserSubtopic>> GetAllUserSubTopicByTopicIdandEmailAsync(int id, string email)
        {
            return await _context.UserSubtopics
                .Include(ust => ust.Subtopic) // Include Subtopic navigation property
                .ThenInclude(subtopic => subtopic.Topic) // Include Topic navigation property within Subtopic
                .Include(ust => ust.User) // Include User navigation property
                .Where(ust => ust.Subtopic.TopicId == id && ust.User.Email == email) // Apply filters
                .ToListAsync();
        }

        public async Task<SubTask?> GetSubTaskByIdAsync(int id)
        {
            return await _context.SubTasks.Include(st => st.SubTopic).FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task<IEnumerable<SubTask>> GetAllSubTaskAsync()
        {
            return await _context.SubTasks.Include(st => st.SubTopic).ToListAsync();
        }

        public async Task<bool> AddSubTaskAsync(SubTask subTask)
        {
            await _context.SubTasks.AddAsync(subTask);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows < 1)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateSubTaskAsync(SubTask subTask)
        {
            _context.SubTasks.Update(subTask);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows < 1)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteSubTaskAsync(int id)
        {
            var subTask = await GetSubTaskByIdAsync(id);
            _context.SubTasks.Remove(subTask);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows < 1)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<SubTask>> GetAllSubTaskBySubTopicIdAsync(int id)
        {
            return await _context.SubTasks
                                 .Where(subtask => subtask.SubTopicId == id)
                                 .Include(st => st.SubTopic)
                                 .OrderByDescending(st => st.CreatedOn)
                                 .ToListAsync();
        }

        public async Task<UserSubtopic> GetUserSubtopicByEmailandSubtopicIdAsync(string email, int subtopicId)
        {
            return await _context.UserSubtopics
                .Where(usersubtopic => usersubtopic.User.Email == email && usersubtopic.SubtopicId == subtopicId)
                .Include(user => user.User)
                                 .Include(st => st.Subtopic)
                                 .FirstOrDefaultAsync();
        }

        public async Task UpdateUserSubtopicAsync(UserSubtopic userSubtopic) 
        {
            _context.UserSubtopics.Update(userSubtopic);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserSubtopic>> GetUserSubtopicByEmailAsync(string email)
        {
            return await _context.UserSubtopics
                                 .Include(us => us.User) // Ensure User is included
                                 .Include(us => us.Subtopic) // Ensure Subtopic is included
                                 .Where(us => us.User != null && us.User.Email == email) // Defensive check
                                 .ToListAsync();
        }
    }
}
 