using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangent.Web.Models
{
    public class Employee
    {
        public User User { get; set; }
        public string Phone_number { get; set; }
        public string Email { get; set; }
        public string Github_user { get; set; }
        public DateTime Birth_date { get; set; }
        public Gender Gender { get; set; }
        public Race Race { get; set; }
        public int Years_worked { get; set; }
        public int Age { get; set; }
        public int Days_to_birthday { get; set; }
        public Position Position { get; set; }
    }
}
