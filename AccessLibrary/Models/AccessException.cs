using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLibrary.Models
{
    public class AccessException:Exception
    {
        public AccessException(string msg):base(msg) { }
    }
}
