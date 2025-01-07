using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.Model;
using SkillUpBackend.Service;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TraineeController : Controller
    {
        private readonly ITraineeService _traineeService;

        public TraineeController(ITraineeService traineeService)
        {
            _traineeService = traineeService;
        }

        // GET: api/Topic
        [HttpGet("Topic")]
        public async Task<IActionResult> GetAllTopic()
        {
            IEnumerable<TopicViewModel> topics = await _traineeService.GetAllTopicAsync();
            if (topics == null || !topics.Any())
            {
                return NotFound("No Topics Found");
            }
            return Ok(topics);
        }

        // GET: api/Trainee/Topic/{id}/SubTopic
        [HttpGet("Topic/{id}/Email/{email}/SubTopic")]
        public async Task<IActionResult> GetAllSubTopicByTopicIdandEmail(int id, string email)
        {
            var subTopics = await _traineeService.GetAllSubTopicByTopicIdandEmailAsync(id, email);

            if (subTopics == null || !subTopics.Any())
            {
                return NotFound("No subtopics found.");
            }
            return Ok(subTopics);
        }

        // GET: api/Trainee/SubTask/{id}
        [HttpGet("SubTask/{id}")]
        public async Task<IActionResult> GetSubTaskById(int id)
        {
            var subTaskViewModel = await _traineeService.GetSubTaskViewModelByIdAsync(id);

            if (subTaskViewModel == null) return NotFound("SubTask not found!");

            return Ok(subTaskViewModel);
        }

        // Get: api/SubTask
        [HttpGet("SubTask")]
        public async Task<IActionResult> GetAllSubTasks()
        {
            var subTaskViewModels = await _traineeService.GetAllSubTaskViewModelAsync();

            return Ok(subTaskViewModels);
        }

        [HttpPost("SubTask")]
        public async Task<IActionResult> CreateSubTask([FromBody] CreationSubTaskViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _traineeService.CreateSubTaskAsync(viewModel);
                if (response == true)
                {
                    return CreatedAtAction(nameof(GetSubTaskById), new { id = viewModel.SubTopicId }, viewModel);
                }

                return BadRequest(new { message = "Failed to create the SubTask. Please verify the provided details." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("SubTask/{id}")]
        public async Task<IActionResult> UpdateSubTask(int id, [FromBody] JsonPatchDocument<SubTask> viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _traineeService.UpdateSubTaskAsync(id, viewModel);
                if (response == true)
                {
                    return CreatedAtAction(nameof(GetSubTaskById), new { id }, viewModel);
                }

                return BadRequest(new { message = "Failed to update the SubTask. Please verify the provided details." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("SubTask/{id}")]
        public async Task<IActionResult> DeleteSubTask(int id)
        {
            try
            {
                var response = await _traineeService.DeleteSubTaskAsync(id);
                if (response == true)
                {
                    return NoContent();
                }

                return BadRequest(new { message = "Failed to delete the SubTask. Please verify the provided details." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Trainee/SubTopic/{id}/SubTask
        [HttpGet("SubTopic/{id}/SubTask")]
        public async Task<IActionResult> GetAllSubTaskBySubTopic(int id)
        {
            var subTasks = await _traineeService.GetAllSubTasksBySubTopicIdAsync(id);

            if (subTasks == null || !subTasks.Any())
            {
                return NotFound("No subtopics found.");
            }
            return Ok(subTasks);
        }

        // GET: api/Trainee/UserSubtopic/{id}
        [HttpGet("UserSubtopic/{email}")]
        public async Task<IActionResult> GetUserSubtopicByEmail(string email)
        { 
            var userSubtopics = await _traineeService.GetUserSubtopicByEmailAsync(email);

            if (userSubtopics == null)
            {
                return NotFound("No usertopic is found for this email.");
            }
            return Ok(userSubtopics);
        }
    }
}
