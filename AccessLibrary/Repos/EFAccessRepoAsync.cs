﻿using AccessLibrary.Models;
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
            
            try
            {
                AspNetUserRole userrole = await(from r in ctx.AspNetUserRoles where r.UserId == id & r.RoleId == role select r).FirstAsync();
                ctx.AspNetUserRoles.Remove(userrole);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException(ex.InnerException.Message);
            }
        }

        public Task<List<AspNetRole>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AspNetUserRole>> GetAllUserRolesAsync()
        {
            List<AspNetUserRole> userroles = new List<AspNetUserRole>();    
            userroles = await ctx.AspNetUserRoles.ToListAsync();
            return userroles;
        }

        public Task<List<AspNetUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task updateRoleAsync(string id, string role)
        {
            throw new NotImplementedException();
        }

        public Task updateRoleAsync(string id, AspNetRole role)
        {
            throw new NotImplementedException();
        }
    }
}
