using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.CustomException;
using SkillUpBackend.Model;
using SkillUpBackend.Service;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _batchService;
        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatches()
        {
            try
            {
                var batches = await _batchService.GetBatches();
                if (batches == null)
                {
                    return NotFound();
                }
                return Ok(batches);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/Users")]
        public async Task<ActionResult<BatchViewModel>> GetBatchByIdWithUsers(int id)
        {
            try
            {
                var batchViewModel = await _batchService.GetBatchByIdWithUsers(id);
                return Ok(batchViewModel);
            }
            catch (BatchNotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Batch>> UpdateBatch(int id, BatchCreateOrEdit batchCreateOrEdit)
        {
            try
            {
                await _batchService.UpdateBatch(id, batchCreateOrEdit);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateBatch(BatchCreateOrEdit batchCreateOrEdit)
        {
            try
            {
                int batchId = await _batchService.CreateBatch(batchCreateOrEdit);
                return Ok(batchId);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBatch(int id)
        {
            try
            {
                await _batchService.DeleteBatch(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("{id}/AddUser")]
        public async Task<ActionResult> AddUserToBatch(BatchCreateOrEdit batchCreateOrEdit, int id)
        {
            try
            {
                await _batchService.AddUserToBatch(batchCreateOrEdit, id);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound();
            }
            catch (BatchNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{id}/RemoveUser")]
        public async Task<ActionResult> RemoveUserFromBatch(BatchCreateOrEdit batchCreateOrEdit, int id)
        {
            try
            {
                await _batchService.RemoveUserFromBatch(batchCreateOrEdit, id);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound();
            }
            catch (BatchNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
