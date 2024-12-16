﻿using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.Model;
using SkillUpBackend.Service;


namespace SkillUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService _service;

        // Inject the service into the controller
        public MentorController(IMentorService service)
        {
            _service = service;
        }

        // GET: api/mentor/topics
        [HttpGet("topics")]
        public IActionResult GetTopics(int pageNumber = 1, int pageSize = 5)
        {
            var topics = _service.GetTopics()
                               .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            if (topics == null || !topics.Any())
            {
                return NotFound(new { success = false, message = "No topics found." });
            }
            return Ok(new { success = true, topics });
        }

        // GET: api/mentor/topic/{id}
        [HttpGet("topic/{id}")]
        public IActionResult GetTopic(int id)
        {
            var topic = _service.GetTopicById(id);
            if (topic == null)
            {
                return NotFound(new { success = false, message = "Topic not found." });
            }
            return Ok(new { success = true, topic });
        }

        // POST: api/mentor/topic
        [HttpPost("topic")]
        public IActionResult AddTopic([FromBody] Topic request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new { success = false, error = "Topic name is required." });
            }

            try
            {
                var topic = new Topic
                {
                    Name = request.Name,
                    Description = request.Description,
                    CreatedOn = DateTime.Now
                };
                _service.AddTopic(topic);
                return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, new { success = true, topic });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        // PUT: api/mentor/topic
        [HttpPut("topic/{id}")]
        public IActionResult EditTopic(int id, [FromBody] Topic request)
        {
            if (request == null || request.Id <= 0)
            {
                return BadRequest(new { success = false, error = "Invalid topic data." });
            }
            if (id != request.Id)
            {
                return BadRequest(new { success = false, error = "ID mismatch." });
            }

            _service.EditTopic(request);
            return Ok(new { success = true, message = "Topic updated successfully." });
        }

        // DELETE: api/mentor/topic/{id}
        [HttpDelete("topic/{id}")]
        public IActionResult DeleteTopic(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, error = "Invalid topic ID." });
            }

            _service.DeleteTopic(id);
            return Ok(new { success = true, message = "Topic deleted successfully." });
        }

        // GET: api/mentor/subtopics/{topicId}
        [HttpGet("subtopics")]
        public IActionResult GetSubtopics()
        {
            var subtopics = _service.GetSubtopics();
            return Ok(new { success = true, subtopics });
        }

        // POST: api/mentor/subtopic
        [HttpPost("subtopic")]
        public IActionResult AddSubtopic([FromBody] Subtopic request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new { success = false, error = "Subtopic name is required." });
            }

            var subtopic = new Subtopic
            {
                Name = request.Name,
                Description = request.Description,
                CreatedOn = DateTime.Now,
                TopicId = request.TopicId
            };

            _service.AddSubtopic(subtopic);
            return CreatedAtAction(nameof(GetSubtopics), new { topicId = subtopic.TopicId }, new { success = true, subtopic });
        }

        // PUT: api/mentor/subtopic
        [HttpPut("subtopic")]
        public IActionResult EditSubtopic([FromBody] Subtopic request)
        {
            _service.EditSubtopic(request);
            return Ok(new { success = true, message = "Subtopic updated successfully." });
        }

        // DELETE: api/mentor/subtopic/{id}
        [HttpDelete("subtopic/{id}")]
        public IActionResult DeleteSubtopic(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, error = "Invalid subtopic ID." });
            }

            _service.DeleteSubtopic(id);
            return Ok(new { success = true, message = "Subtopic deleted successfully." });
        }
    }
}
