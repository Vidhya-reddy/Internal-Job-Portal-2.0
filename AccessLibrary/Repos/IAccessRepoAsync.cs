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
        Task addRole(string id, string role);
        Task updateRole(string id, string role);
        Task deleteRole(string id);
        Task<List<AspNetUser>> GetAllUsers();
        Task addUserRoleAsync(AspNetUserRole userRole);  
        Task updateUserRoleAsync(AspNetUserRole userRole);
        Task deleteUserRoleAsync(AspNetUserRole userRole);
    }
}
