using AccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLibrary.Repos
{
    public interface IAccessRepoAsync
    {
        Task<List<AspNetUserRole>> GetAllUserRolesAsync();
        Task addRoleAsync(AspNetRole role);
        Task<AspNetRole> GetRoleAsync(string roleId);
        Task updateRoleAsync(string id, string role);
        Task deleteRoleAsync(string id);
        Task<List<AspNetRole>> GetAllRolesAsync();  
        Task<List<AspNetUser>> GetAllUsers(); 
        Task<AspNetUserRole> GetUserRoleAsync(string id,string role);   

        Task addUserRoleAsync(AspNetUserRole userRole);  
        Task deleteUserRoleAsync(string id, string role);
    }
}
