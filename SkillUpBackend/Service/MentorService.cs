using SkillUpBackend.Model;
using SkillUpBackend.Repository;
using SkillUpBackend.Service;

namespace skillup.Api.Service
{
    public class MentorService:IMentorService
    {
        private readonly IMentorRepository _repository;

        public MentorService(IMentorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Topic> GetTopics()
        {
            return _repository.GetActiveTopics();
        }

        public Topic? GetTopicById(int id)
        {
            return _repository.GetTopicById(id);
        }

        public void AddTopic(Topic topic)
        {
            topic.CreatedOn = DateTime.Now;
            _repository.AddTopic(topic);
        }

        public void EditTopic(Topic topic)
        {
            var existingTopic = _repository.GetTopicById(topic.Id);
            if (existingTopic != null)
            {
                existingTopic.Name = topic.Name;
                existingTopic.Description = topic.Description;
                existingTopic.UpdatedOn = DateTime.Now;
                _repository.UpdateTopic(existingTopic);
            }
        }

        public void DeleteTopic(int id)
        {
            _repository.DeactivateTopic(id);
        }

        public IEnumerable<Subtopic> GetSubtopics()
        {
            return _repository.GetActiveSubtopics();
        }

        public Subtopic? GetSubtopicById(int id)
        {
            return _repository.GetSubtopicById(id);
        }

        public void AddSubtopic(Subtopic subtopic)
        {
            subtopic.CreatedOn = DateTime.Now;
            _repository.AddSubtopic(subtopic);
        }

        public void EditSubtopic(Subtopic subtopic)
        {
            var existingSubtopic = _repository.GetSubtopicById(subtopic.Id);
            if (existingSubtopic != null)
            {
                existingSubtopic.Name = subtopic.Name;
                existingSubtopic.Description = subtopic.Description;
                existingSubtopic.UpdatedOn = DateTime.Now;
                _repository.UpdateSubtopic(existingSubtopic);
            }
        }

        public void DeleteSubtopic(int id)
        {
            _repository.DeactivateSubtopic(id);
        }
    }
}
