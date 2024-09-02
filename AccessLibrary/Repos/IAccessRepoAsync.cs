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
        Task addUserRoleAsync(AspNetUserRole userRole);  
    }
}
