using AccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLibrary.Repos
{
    public class EFAccessRepoAsync : IAccessRepoAsync
    {
        AccessDBContext ctx = new AccessDBContext();

        public Task addRole(string id, string role)
        {
            throw new NotImplementedException();
        }

        public async Task addUserRoleAsync(AspNetUserRole userRole)
        {
           await ctx.AspNetUserRoles.AddAsync(userRole);
           await ctx.SaveChangesAsync();    
           
        }

        public Task deleteRole(string id)
        {
            throw new NotImplementedException();
        }

        public Task deleteUserRoleAsync(AspNetUserRole userRole)
        {
            throw new NotImplementedException();
        }

        public Task updateRole(string id, string role)
        {
            throw new NotImplementedException();
        }

        public Task updateUserRoleAsync(AspNetUserRole userRole)
        {
            throw new NotImplementedException();
        }
    }
}
