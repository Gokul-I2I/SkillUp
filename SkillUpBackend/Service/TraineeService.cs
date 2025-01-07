using Microsoft.AspNetCore.JsonPatch;
using SkillUpBackend.Mapper;
using SkillUpBackend.Model;
using SkillUpBackend.Repository;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Service
{
    public class TraineeService : ITraineeService
    {
        private readonly ITraineeRepository _traineeRepository;

        public TraineeService(ITraineeRepository repository)
        {
            _traineeRepository = repository;
        }

        public async Task<IEnumerable<TopicViewModel>> GetAllTopicAsync()
        {
            // Fetch all topics from the repository
            var topics = await _traineeRepository.GetAllTopicAsync();

            // Check if the topics are null or empty
            if (topics == null || !topics.Any())
            {
                return Enumerable.Empty<TopicViewModel>();
            }

            // Convert Topic models to TopicViewModel using the static method
            var topicViewModels = topics.Select(topic => TopicMapper.ConvertToTopicViewModel(topic));

            return topicViewModels;
        }

        public async Task<IEnumerable<SubTopicViewModel>> GetAllSubTopicByTopicIdandEmailAsync(int topicId, string email)
        {
            var userSubTopics = await _traineeRepository.GetAllUserSubTopicByTopicIdandEmailAsync(topicId, email);

            // Check if the SubTopics are null or empty
            if (userSubTopics == null || !userSubTopics.Any())
            {
                return Enumerable.Empty<SubTopicViewModel>();
            }

            // Convert SubTopics to SubTopicViewModel using the static method
            var subTopicViewModels = userSubTopics.Select(userSubTopic => SubTopicMapper.ConvertToSubTopicViewModel(userSubTopic));

            return subTopicViewModels;
        }

        public async Task<SubTaskViewModel?> GetSubTaskViewModelByIdAsync(int id)
        {
            var subTask = await _traineeRepository.GetSubTaskByIdAsync(id);
            if (subTask == null) return null;
            SubTaskViewModel subTaskViewModel = SubTaskMapper.ConvertToSubTaskViewModel(subTask);
            return subTaskViewModel;
        }

        public async Task<IEnumerable<SubTaskViewModel>> GetAllSubTaskViewModelAsync()
        {
            var subTasks = await _traineeRepository.GetAllSubTaskAsync();
            // Check if the topics are null or empty
            if (subTasks == null || !subTasks.Any())
            {
                return Enumerable.Empty<SubTaskViewModel>();
            }
            var subTaskViewModels = subTasks.Select(subTask => SubTaskMapper.ConvertToSubTaskViewModel(subTask));
            return subTaskViewModels;
        }

        public async Task UpdateTimeTaken(int subtopicId, string email)
        {
            // Fetch all related subtasks for the subtopic
            var subTasks = await _traineeRepository.GetAllSubTaskBySubTopicIdAsync(subtopicId);

            // Calculate the total hours and minutes
            int totalHours = subTasks.Sum(task => task.Hours);
            int totalMinutes = subTasks.Sum(task => task.Minutes);

            // Convert minutes to hours if they exceed 60
            totalHours += totalMinutes / 60;
            totalMinutes %= 60;

            // Update the UserSubtopic's TimeTaken field
            var userSubtopic = await _traineeRepository.GetUserSubtopicByEmailandSubtopicIdAsync(email, subtopicId);
            if (userSubtopic != null)
            {
                userSubtopic.TimeTaken = $"{totalHours} hours {totalMinutes} minutes";

                // Save changes
                await _traineeRepository.UpdateUserSubtopicAsync(userSubtopic);
            }
        }

        public async Task<bool> CreateSubTaskAsync(CreationSubTaskViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var subTask = new SubTask
            {
                Title = viewModel.Title,
                SubTopicId = viewModel.SubTopicId,
                Hours = viewModel.Hours, 
                Minutes = viewModel.Minutes,
                CreatedOn = DateTime.Now,
                CreatedBy = viewModel.CreatedBy
            };

            var response = await _traineeRepository.AddSubTaskAsync(subTask);

            if (response == true)
            {
                // Update the UserSubtopic's TimeTaken
                await UpdateTimeTaken(viewModel.SubTopicId, viewModel.CreatedBy);
            }
            return response;
        }

        public async Task<bool> UpdateSubTaskAsync(int id, JsonPatchDocument<SubTask> viewModel)
        {
            var existingSubTask = await _traineeRepository.GetSubTaskByIdAsync(id);
            if (existingSubTask == null)
                throw new KeyNotFoundException($"SubTask with ID {id} not found.");

            viewModel.ApplyTo(existingSubTask);
            existingSubTask.UpdatedOn = DateTime.Now;
            var response = await _traineeRepository.UpdateSubTaskAsync(existingSubTask);

            if (response == true)
            {
                // Update the UserSubtopic's TimeTaken
                await UpdateTimeTaken(existingSubTask.SubTopicId, existingSubTask.CreatedBy);
            }
            return response;
        }

        public async Task<bool> DeleteSubTaskAsync(int id)
        {
            var existingSubTask = await _traineeRepository.GetSubTaskByIdAsync(id);
            if (existingSubTask == null) throw new KeyNotFoundException($"SubTask with ID {id} not found.");

            return await _traineeRepository.DeleteSubTaskAsync(id);
        }

        public async Task<IEnumerable<SubTaskViewModel>> GetAllSubTasksBySubTopicIdAsync(int id)
        {
            var subTasks = await _traineeRepository.GetAllSubTaskBySubTopicIdAsync(id);

            // Check if the SubTopics are null or empty
            if (subTasks == null || !subTasks.Any())
            {
                return Enumerable.Empty<SubTaskViewModel>();
            }

            // Convert SubTopics to SubTopicViewModel using the static method
            var subTaskViewModels = subTasks.Select(subTask => SubTaskMapper.ConvertToSubTaskViewModel(subTask));

            return subTaskViewModels;
        }

        public async Task<IEnumerable<UserSubtopicViewModel>> GetUserSubtopicByEmailAsync(string email)
        {
            var userSubtopics = await _traineeRepository.GetUserSubtopicByEmailAsync(email);
            if (userSubtopics == null || !userSubtopics.Any())
            {
                return Enumerable.Empty<UserSubtopicViewModel>();
            }
            var userSubtopicViewModels = userSubtopics.Select(userSubtopic => UserSubtopicMapper.ConvertToUserSubtopicViewModel(userSubtopic));
            return userSubtopicViewModels;
        }
    }
}
