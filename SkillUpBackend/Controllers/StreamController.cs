using Microsoft.AspNetCore.Mvc;

namespace SkillUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        public StreamController() { }   
        [HttpGet]
        public async Task<ActionResult<List<Stream>>> GetAllStreams()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public void GetStreamById(int id)
        {
        }

        [HttpPost]
        public void Post([FromBody] Stream stream)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Stream stream)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
