using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJPMvcApp.Models
{
    public class AspNetUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public virtual AspNetUser? User { get; set; }
        public virtual AspNetRole? Role { get; set; }
    }
}
