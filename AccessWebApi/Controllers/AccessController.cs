using AccessLibrary.Models;
using AccessLibrary.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        IAccessRepoAsync repo;
        public AccessController(IAccessRepoAsync repository)
        {
            repo = repository;
        }
        [HttpPost]
        public async Task<ActionResult> Insert(AspNetUserRole userRoles)
        {
            try
            {
                await repo.addUserRoleAsync(userRoles);
                return Created($"api/ApplyJob/{userRoles.UserId}/{userRoles.RoleId}/", userRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
