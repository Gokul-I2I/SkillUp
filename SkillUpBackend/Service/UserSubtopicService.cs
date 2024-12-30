using Microsoft.EntityFrameworkCore;
using SkillUpBackend.Model;

namespace SkillUpBackend.Service
{
    public class UserSubtopicService : IUserSubtopicService
    {
        private readonly ApplicationDbContext _context;

        public UserSubtopicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserSubtopic>> GetUserSubtopicsAsync()
        {
            List<UserSubtopic> data = new List<UserSubtopic>();
            try
            {
                data = await _context.UserSubtopics
                    .Include(us => us.User)
                    .ThenInclude(u => u.Role) // Ensure Role is included
                    .Include(us => us.Subtopic)
                    .Where(us => us.User.Role.Name == "trainee") // Uncommented for filtering
                    .ToListAsync();

                //Console.WriteLine($"Fetched {data.Count} UserSubtopics.");
                //foreach (var us in data)
                //{
                //    Console.WriteLine($"UserSubtopic: UserId={us.UserId}, SubtopicId={us.SubtopicId}");
                //}

            }
            catch (Exception e)
            {
                // Log the error properly
                Console.WriteLine($"Error fetching UserSubtopics: {e.Message}");
            }
            Console.WriteLine(data);
            return data;
        }

        public async Task<bool> UpdateUserSubtopicStateAsync(int userId, int subtopicId, TaskState state)
        {
            var userSubtopic = await _context.UserSubtopics
                .FirstOrDefaultAsync(us => us.UserId == userId && us.SubtopicId == subtopicId);

            if (userSubtopic != null)
            {
                userSubtopic.State = state;
                _context.UserSubtopics.Update(userSubtopic);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
   