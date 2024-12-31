using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.CustomException;
using SkillUpBackend.Model;
using SkillUpBackend.Service;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private IStreamService _streamService;
        public StreamController(StreamService streamService)
        {
            _streamService = streamService;
        }
        [HttpGet]
        public async Task<ActionResult<List<StreamModel>>> GetAllStreams()
        {
            try
            {
                var streams = await _streamService.GetAllStreams();
                return Ok(streams);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StreamModel>> GetStreamById(int id)
        {
            try
            {
                var streamModel = await _streamService.GetStreamById(id);
                return Ok(streamModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddStream(StreamCreateOrEdit stream)
        {
            try
            {
                await _streamService.AddStream(stream);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStream(int id, StreamCreateOrEdit streamModel)
        {
            try
            {
                await _streamService.UpdateStream(id, streamModel);
                return Ok();
            }
            catch (StreamNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStream(int id)
        {
            try
            {
                await _streamService.DeleteStream(id);
                return NoContent();
            }
            catch (StreamNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
