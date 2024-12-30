using Microsoft.EntityFrameworkCore;
using SkillUpBackend.Model;
namespace SkillUpBackend.Repository
{
    public class MentorRepository : IMentorRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public MentorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Topic> GetActiveTopics()
        {
            return _dbContext.Topics
                             .Where(t => t.IsActive) // Only active topics
                             .Select(t => new Topic
                             {
                                 Id = t.Id,
                                 Name = t.Name,
                                 Description = t.Description
                             }) // Return only required fields
                                    .ToList();
        }

        public Topic? GetTopicById(int id)
        {
            return _dbContext.Topics.Find(id);
        }

        public void AddTopic(Topic topic)
        {
            _dbContext.Topics.Add(topic);
            _dbContext.SaveChanges();
        }

        public void UpdateTopic(Topic topic)
        {
            _dbContext.SaveChanges();
        }

        public void DeactivateTopic(int id)
        {
            var topic = _dbContext.Topics.Find(id);
            if (topic != null)
            {
                topic.IsActive = false;
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Subtopic> GetActiveSubtopics()
        {
            return _dbContext.Subtopics
                .Where(s => s.IsActive) // Only active subtopics
                .Include(s => s.Topic)  // Make sure the related Topic is loaded
                .Select(s => new Subtopic
                {
                    Id = s.Id,
                    TopicId = s.TopicId,  // Ensure TopicId is correctly set
                    Name = s.Name,
                    Description = s.Description,
                    IsActive = s.IsActive,
                    CreatedOn = s.CreatedOn,
                    UpdatedOn = s.UpdatedOn
                })
                .ToList();

        }

        public Subtopic? GetSubtopicById(int id)
        {
            return _dbContext.Subtopics.Find(id);
        }

        public void AddSubtopic(Subtopic subtopic)
        {
            _dbContext.Subtopics.Add(subtopic);
            _dbContext.SaveChanges();
        }

        public void UpdateSubtopic(Subtopic subtopic)
        {
            _dbContext.SaveChanges();
        }

        public void DeactivateSubtopic(int id)
        {
            var subtopic = _dbContext.Subtopics.Find(id);
            if (subtopic != null)
            {
                subtopic.IsActive = false;
                _dbContext.SaveChanges();
            }
        }
    }
}
