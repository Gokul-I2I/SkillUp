using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.Model;
using SkillUpBackend.Service;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            try
            {
                var roles = _roleService.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole([FromBody] RoleCreateModel roleCreate)
        {
            await _roleService.AddRole(roleCreate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteRole(id);
            return NoContent();
        }
    }
}
