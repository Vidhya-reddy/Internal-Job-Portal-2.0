using AccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task addRoleAsync(AspNetRole role)
        {
            try
            {
                await ctx.AspNetRoles.AddAsync(role);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex) { 
                throw new AccessException("The role ID you have entered is already in use. Please check the ID and try again.");
            }
           
        }
        public async Task addUserRoleAsync(AspNetUserRole userRole)
        {
            try
            {
                await ctx.AspNetUserRoles.AddAsync(userRole);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException("The Role for this User is already in use. Please check the ID and try again.");
            }
        }
        public async Task deleteRoleAsync(string id)
        {
            try
            {
                AspNetRole role = await (from r in ctx.AspNetRoles where r.Id == id select r).FirstAsync();
                ctx.AspNetRoles.Remove(role);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException("The Role ID cannot be deleted because it is used in UserRole Table." +
                    " Please check and remove any related information before trying to delete it again");
            }
        }

        public async Task deleteUserRoleAsync(string id, string role)
        {
            if (role != "Admin")
            {
                AspNetUserRole userrole = await (from r in ctx.AspNetUserRoles where r.UserId == id & r.RoleId == role select r).FirstAsync();
                ctx.AspNetUserRoles.Remove(userrole);
                ctx.SaveChangesAsync();
            }
            else
            {
                throw new AccessException("You cannot delete the Admin");
            }
            
        }

        public async Task<List<AspNetRole>> GetAllRolesAsync()
        {
            List<AspNetRole> roles = await ctx.AspNetRoles.ToListAsync();
            return roles;
        }

        public async Task<List<AspNetUserRole>> GetAllUserRolesAsync()
        {
            List<AspNetUserRole> userroles = new List<AspNetUserRole>();    
            userroles = await ctx.AspNetUserRoles.ToListAsync();
            return userroles;
        }

        public async  Task<List<AspNetUser>> GetAllUsers()
        {
            List<AspNetUser> users = await ctx.AspNetUsers.ToListAsync();
            return users;
        }

        public async Task updateRoleAsync(string id, string role)
        {             
             if (id != "1")
             {
                AspNetRole roles = await (from r in ctx.AspNetRoles where r.Id == id select r).FirstAsync();
                roles.Name = role;
                await ctx.SaveChangesAsync();
             }
             else
             {
                throw new AccessException("Cannot Update Admin Role");
             }                           
        }      
    }
}
