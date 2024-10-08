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
            List<AspNetUserRole> userRoles = await(from e in ctx.AspNetUserRoles where e.RoleId == id  select e).ToListAsync();
            if (userRoles.Count == 0)
            {
                AspNetRole role = await (from r in ctx.AspNetRoles where r.Id == id select r).FirstAsync();
                ctx.AspNetRoles.Remove(role);
                await ctx.SaveChangesAsync();
            }
            else
                throw new AccessException("The Role ID cannot be deleted because it is used in UserRole Table." +
                    " Please check and remove any related information before trying to delete it again");
            
        }

        public async Task deleteUserRoleAsync(string id, string role)
        {
            if (role != "1")
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

        public async Task<AspNetRole> GetRoleAsync(string roleId)
        {
            try
            {
                AspNetRole role = await(from j in ctx.AspNetRoles where j.Id == roleId select j).FirstAsync();
                return role;
            }
            catch
            {
                throw new AccessException("No Such RoleId Found");
            }
        }

        public async  Task<AspNetUserRole> GetUserRoleAsync(string id, string role)
        {
            try
            {
                AspNetUserRole Userrole = await(from j in ctx.AspNetUserRoles where j.UserId == id && j.RoleId == role  select j).FirstAsync();
                return Userrole;
            }
            catch
            {
                throw new AccessException("No Such RoleId Found");
            }
        }

        public async Task updateRoleAsync(string id, AspNetRole role)
        {             
             if (id != "1")
             {
                AspNetRole roles = await (from r in ctx.AspNetRoles where r.Id == id select r).FirstAsync();
                roles.Name = role.Name;
                await ctx.SaveChangesAsync();
             }
             else
             {
                throw new AccessException("Cannot Update Admin Role");
             }                           
        }      
    }
}
