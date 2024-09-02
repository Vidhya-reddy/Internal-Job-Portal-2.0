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
        public async Task addUserRoleAsync(AspNetUserRole userRole)
        {
           await ctx.AspNetUserRoles.AddAsync(userRole);
           await ctx.SaveChangesAsync();    
           
        }
    }
}
