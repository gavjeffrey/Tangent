using System;
using System.ComponentModel.DataAnnotations;

namespace Tangent.Web.Models.ManageViewModels
{
    public class ProfileViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string StatusMessage { get; set; }

        [Display(Name = "First Name")]
        public string First_name { get; set; }

        [Display(Name = "Surname")]
        public string Last_name { get; set; }

        [Display(Name = "Is Active user")]
        public string Is_active { get; set; }

        [Display(Name = "Is Staff member")]
        public string Is_staff { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone_number { get; set; }

        [Display(Name = "Github User")]
        public string Github_user { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime Birth_date { get; set; }

        public string Gender { get; set; }

        public string Race { get; set; }

        [Display(Name = "Years of service")]
        public int Years_worked { get; set; }

        public int Age { get; set; }

        [Display(Name = "Days left before next birthday")]
        public int Days_to_birthday { get; set; }

        public string Position { get; set; }
    }
}
