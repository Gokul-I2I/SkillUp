using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.CustomException;
using SkillUpBackend.Model;
using SkillUpBackend.Service;

namespace SkillUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public LoginController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        [HttpPost]
        public async Task<IActionResult> LoginValidation([FromBody] LoginRequest request)
        {
            try
            {
                User isValidUser = await _userService.ValidateUserCredentials(request.Email, request.Password);
                return Ok(isValidUser);
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}