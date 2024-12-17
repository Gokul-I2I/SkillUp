using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.CustomException;
using SkillUpBackend.Model;
using SkillUpBackend.Service;

namespace SkillUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // Get : Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                if (users == null)
                {
                    return NotFound("No users found.");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserCreateOrEdit userCreateOrEdit)
        {
            try
            {
                await _userService.AddUser(userCreateOrEdit);
                return Ok();
            }
            catch (UserAlreadyExitException ex)
            {
                return Conflict(ex.Message );
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserCreateOrEdit userCreateOrEdit)
        {
            try
            {
                await _userService.UpdateUser(id, userCreateOrEdit);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var userEditModel = await _userService.GetUserById(id);
                return Ok(userEditModel);
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ActiveUser(int id)
        {
            try
            {
                await _userService.ActiveUser(id);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
        }

        // Delete: Users/Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return NoContent();
            }
            catch (UserNotFoundException)
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