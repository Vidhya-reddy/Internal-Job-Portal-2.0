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
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            List<AspNetUser> users = await repo.GetAllUsers();
            return Ok(users);
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<AspNetUser> users = await repo.GetAllUsersRoles();
            return Ok(users);
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
        [HttpPost("{id}/{role}")]
        public async Task<ActionResult> InsertRole(string id, string role)
        {
            try
            {
                await repo.addRole(id, role);
                return Created($"api/Access/{id}/", role);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("{id}/{role}")]
        public async Task<ActionResult> Update(string id, string role)
        {
            try
            {
                await repo.updateRole(id, role);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
         public async Task<ActionResult> UpdateUserRole(string id)
         {
             try
             {
                 await repo.updateUserRoleAsync(id);
                 return Ok();
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
         {
             try
             {
                 await repo.deleteUserRoleAsync(id);
                 return Ok();
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }
         [HttpDelete("{id}")]
         public async Task<ActionResult> DeleteRole(string id)
         {
             try
             {
                 await repo.deleteRole(id);
                 return Ok();
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }
    }
}
