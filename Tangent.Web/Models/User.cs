using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangent.Web.Models
{
    public class User : IdentityUser
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public bool Is_active { get; set; }
        public bool Is_staff { get; set; }        
    }
}
