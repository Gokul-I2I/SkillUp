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
            var users = _userService.GetAllUsers();
            if (users == null)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
        // POST: Users/Create
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserCreateModel userCreateModel)
        {
            try
            {
                await _userService.AddUser(userCreateModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Delete: Users/Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserEditModel userEditModel)
        {
            try
            {
                await _userService.UpdateUser(id, userEditModel);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
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
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}