using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.CustomException;
using SkillUpBackend.Model;
using SkillUpBackend.Service;
using Microsoft.AspNetCore.Hosting;

namespace SkillUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IWebHostEnvironment _env;

        public LoginController(IUserService userService, IRoleService roleService, IWebHostEnvironment env)
        {
            _userService = userService;
            _roleService = roleService;
            _env = env;
        }
        [HttpPost]
        public async Task<IActionResult> LoginValidation([FromBody] LoginRequest request)
        {
            try
            {
                User isValidUser = await _userService.ValidateUserCredentials(request.Email, request.Password);
                if (isValidUser != null)
                {
                    Console.WriteLine($"Cookie set for: {request.Email}");
                                      
                    return Ok(isValidUser);
                }
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