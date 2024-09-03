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
        [HttpGet("{roleId}")]
        public async Task<ActionResult> GetRole(string roleId)
        {
            AspNetRole role = await repo.GetRoleAsync(roleId);
            return Ok(role);
        }
        [HttpGet("{id}/{role}")]
        public async Task<ActionResult> GetUserRole(string id,string role)
        {
            AspNetUserRole userrole = await repo.GetUserRoleAsync(id, role);
            return Ok(userrole);
        }


        [HttpGet("Users")]
        public async Task<ActionResult> GetAllUsers()
        {
            List<AspNetUser> users = await repo.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("Roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            List<AspNetRole> users = await repo.GetAllRolesAsync();
            return Ok(users);
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<AspNetUserRole> userRoles = await repo.GetAllUserRolesAsync();
            return Ok(userRoles);
        }
        [HttpPost]
        public async Task<ActionResult> Insert(AspNetUserRole userRole)
        {
            try
            {
                await repo.addUserRoleAsync(userRole);
                return Created($"api/Access/{userRole.UserId}/{userRole.RoleId}/", userRole);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost("Role")]
        public async Task<ActionResult> InsertRole(AspNetRole role)
        {
            try
            {
                await repo.addRoleAsync(role);
                return Created($"api/Access/", role);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("{id}/{role}")]
        public async Task<ActionResult> Update(string id,string role)
        {
            try
            {
                await repo.updateRoleAsync(id, role);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}/{role}")]
        public async Task<ActionResult> Delete(string id,string role)
         {
             try
             {
                 await repo.deleteUserRoleAsync(id,role);
                 return Ok();
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }
         [HttpDelete("Role/{id}")]
         public async Task<ActionResult> DeleteRole(string id)
         {
             try
             {
                 await repo.deleteRoleAsync(id);
                 return Ok();
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }
    }
}
