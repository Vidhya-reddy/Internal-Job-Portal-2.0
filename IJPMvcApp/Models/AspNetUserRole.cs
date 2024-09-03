using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJPMvcApp.Models
{
    public class AspNetUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        [Display(Name ="User")]
        public string UserName { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}

